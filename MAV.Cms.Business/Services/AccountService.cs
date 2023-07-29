using AutoMapper;
using FluentValidation.Results;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Exceptions;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Business.Specifications;
using MAV.Cms.Business.Validators;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Common.Interfaces;
using MAV.Cms.Common.Utilities;
using MAV.Cms.Domain.Entities.Identity;
using MAV.Cms.Domain.Interfaces;
using MAV.Cms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class AccountService : IAccountService
    {
        private readonly IMapper _mapper;
        private readonly ILogger<AccountService> _logger;
        private readonly UserManager<MavUser> _userManager;
        private readonly SignInManager<MavUser> _signInManager;
        private readonly ITokenService _tokenService;
        private readonly ICacheService _cacheService;
        private readonly IBaseRepository<MavUser> _appUserRepository;
        private readonly IHttpContextAccessor _httpContext;

        public AccountService(IMapper mapper, ILogger<AccountService> logger, UserManager<MavUser> userManager, SignInManager<MavUser> signInManager, ITokenService tokenService, ICacheService cacheService, IUnitOfWork uow, IHttpContextAccessor httpContext)
        {
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenService = tokenService ?? throw new ArgumentNullException(nameof(tokenService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
            _appUserRepository = uow.Repository<MavUser>();
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }

        public async Task<ApiResponse<UserResponse>> Create(CreateAccountDTO request)
        {
            try
            {
                CreateAccountValidator validator = new CreateAccountValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                if (_userManager.Users.Any(x => x.Email.Equals(request.Email)))
                    throw new Exception(LangManager.Translate("Account.AlreadyUsing"));

                var user = _mapper.Map<MavUser>(request);

                user.CreatedDate = DateTime.Now;
                user.CreatedLocalIp = NetworkUtils.GetLocalIp();
                user.EmailConfirmed = true;
                user.PhoneNumberConfirmed = true;

                var result = await _userManager.CreateAsync(user, request.Password);

                if (!result.Succeeded) throw new Exception(LangManager.Translate("Account.UserNotCreated"));


                var userResponse = new UserResponse
                {
                    Name = user.Name,
                    Token = await _tokenService.CreateToken(user),
                    UserName = user.UserName,
                    Surname = user.Surname,
                };

                return new ApiResponse<UserResponse>(userResponse, true, StatusCodes.Status201Created, LangManager.Translate("Account.Created"));
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<UserResponse>> Login(LoginDTO request)
        {
            try
            {
                LoginValidator validator = new LoginValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                MavUser user = null;
                if (request.EmailOrUserName.Contains("@"))
                    user = await _userManager.FindByEmailAsync(request.EmailOrUserName);
                else
                    user = await _userManager.FindByNameAsync(request.EmailOrUserName);

                if (user == null)
                    throw new UnauthorizedAccessException();

                var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (!result.Succeeded)
                    throw new UnauthorizedAccessException(LangManager.Translate("Account.InvalidPassword"), new Exception(LangManager.Translate("Account.InvalidPassword")));

                var userJson = await _cacheService.GetCacheAsync($"AppUser:{user.Id}");
                if (userJson.HasValue())
                {
                    await _cacheService.RemoveFromCacheAsync($"AppUser:{user.Id}");
                }

                var spec = new AppUserSpec(user.Id);
                var appUserResponse = await _appUserRepository.GetEntityWithSpec<AppUserResponse>(spec, _mapper);

                if (appUserResponse != null)
                {
                    await _cacheService.SetCacheAsync($"AppUser:{user.Id}", appUserResponse, TimeSpan.FromHours(6));
                }

                var userResponse = new UserResponse
                {
                    Name = user.Name,
                    Token = await _tokenService.CreateToken(user),
                    UserName = user.UserName,
                    Surname = user.Surname,
                    ProfilePhotoPath = user.ProfileImagePath.ResolveUrl(),
                };

                return new ApiResponse<UserResponse>(userResponse, true, StatusCodes.Status200OK);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("401", ex);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }

        }

        public async Task<ApiResponse<bool>> Logout()
        {
            try
            {
                var userId = _httpContext.HttpContext.User.GetUserId();
                if (!userId.HasValue)
                    throw new UnauthorizedAccessException();

                var userJson = await _cacheService.GetCacheAsync($"AppUser:{userId.Value}");
                if (userJson.HasValue())
                {
                    await _cacheService.RemoveFromCacheAsync($"AppUser:{userId.Value}");
                }

                return new ApiResponse<bool>(true, true, StatusCodes.Status200OK);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("401", ex);
            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ValidationException(ex.Errors);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
