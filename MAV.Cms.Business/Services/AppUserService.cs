using AutoMapper;
using FluentValidation.Results;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Exceptions;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Business.Specifications;
using MAV.Cms.Business.Validators;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Common.Interfaces;
using MAV.Cms.Common.Utilities;
using MAV.Cms.Domain.Entities.Identity;
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
    public class AppUserService : IAppUserService
    {
        private readonly UserManager<MavUser> _userManager;
        private readonly RoleManager<MavRole> _roleManager;
        private readonly IBaseRepository<MavUser> _appUserRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AppUserService> _logger;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IUploadService _uploadService;
        private readonly ICacheService _cacheService;

        public AppUserService(UserManager<MavUser> userManager, RoleManager<MavRole> roleManager, IUnitOfWork uow, IMapper mapper, ILogger<AppUserService> logger, IHttpContextAccessor httpContext, IUploadService uploadService, ICacheService cacheService)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _appUserRepository = uow.Repository<MavUser>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _httpContext = httpContext ?? throw new ArgumentNullException(nameof(httpContext));
            _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<ApiResponse<AppUserResponse>> Create(CreateAppUserDTO request)
        {
            try
            {
                CreateAppUserValidator validator = new CreateAppUserValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                if (_userManager.Users.Any(x => x.Email.Equals(request.Email)))
                    throw new Exception(LangManager.Translate("Account.AlreadyUsing"));

                var appUser = _mapper.Map<MavUser>(request);
                appUser.CreatedById = _httpContext.HttpContext.User.GetUserId();
                appUser.CreatedDate = DateTime.Now;
                appUser.CreatedLocalIp = NetworkUtils.GetLocalIp();
                appUser.CreatedRemoteIp = NetworkUtils.GetRemoteIp(_httpContext);
                appUser.EmailConfirmed = true;
                appUser.PhoneNumberConfirmed = true;

                if (request.ProfileImageFile != null && request.ProfileImageFile.Length > 0)
                {
                    var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                    {
                        DataId = appUser.Id,
                        ModelName = nameof(MavUser),
                        File = request.ProfileImageFile,
                    });
                    if (uploadResponse.IsSuccess)
                        appUser.ProfileImagePath = uploadResponse.Url;
                }

                var result = await _userManager.CreateAsync(appUser, request.Password);

                if (!result.Succeeded) throw new Exception(LangManager.Translate("Account.UserNotCreated"));

                if (request.UserRoleIdList != null && request.UserRoleIdList.Any())
                {
                    var roles = _roleManager.Roles.Where(x => request.UserRoleIdList.Contains(x.Id)).ToList();
                    if (roles != null && roles.Any())
                    {
                        for (int i = 0; i < roles.Count; i++)
                        {
                            await _userManager.AddToRoleAsync(appUser, roles[i].Name);
                        }
                    }
                }

                var spec = new AppUserSpec(appUser.Id);
                var appUserResponse = await _appUserRepository.GetEntityWithSpec<AppUserResponse>(spec, _mapper);

                return new ApiResponse<AppUserResponse>(appUserResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("AppUser.ControllerTitle")));
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

        public async Task<ApiResponse<bool>> Delete(BaseDeleteDTO request)
        {
            //Servislere deleteList yaz
            try
            {
                var appUser = await _userManager.FindByIdAsync(request.Id.ToString());
                if (appUser == null)
                    throw new NotFoundException("AppUser", request.Id);


                bool result = false;

                if (request.IsSoftDelete)
                {
                    appUser.isSoftDelete = true;
                    appUser.DeletedById = _httpContext.HttpContext.User.GetUserId();
                    appUser.DeletedDate = DateTime.Now;
                    appUser.DeletedLocalIp = NetworkUtils.GetLocalIp();
                    appUser.DeletedRemoteIp = NetworkUtils.GetRemoteIp(_httpContext);
                    var updateResult = await _userManager.UpdateAsync(appUser);
                    result = updateResult.Succeeded;
                }
                else
                {
                    var deleteResult = await _userManager.DeleteAsync(appUser);
                    result = deleteResult.Succeeded;
                }

                return new ApiResponse<bool>(result, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("AppUser.ControllerTitle")));
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("404", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<AppUserResponse>> GetById(Guid Id)
        {
            try
            {
                var spec = new AppUserSpec(Id);
                var appUserResponse = await _appUserRepository.GetEntityWithSpec<AppUserResponse>(spec, _mapper);
                if (appUserResponse == null)
                    throw new NotFoundException("Page", Id);

                return new ApiResponse<AppUserResponse>(appUserResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("404", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<AppUserResponse>> GetAll(GetAllAppUserSpecDTO request)
        {
            try
            {
                var spec = new AppUserSpec(request);
                var specCount = new AppUserSpecCount(request);
                var appUserCount = await _appUserRepository.CountAsync(specCount);
                var appUserResponse = await _appUserRepository.ListWithSpecAsync<AppUserResponse>(spec, _mapper);

                return new ApiResponse<AppUserResponse>(request.PageIndex, request.PageSize, appUserCount, appUserResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<AppUserResponse>> Update(UpdateAppUserDTO request)
        {
            try
            {
                var appUser = await _userManager.FindByIdAsync(request.Id.ToString());
                if (appUser == null)
                    throw new NotFoundException("AppUser", request.Id);


                UpdateAppUserValidator validator = new UpdateAppUserValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                _mapper.Map(request, appUser, typeof(UpdateAppUserDTO), typeof(MavUser));

                appUser.UpdatedById = _httpContext.HttpContext.User.GetUserId();
                appUser.UpdatedDate = DateTime.Now;
                appUser.UpdatedLocalIp = NetworkUtils.GetLocalIp();
                appUser.UpdatedRemoteIp = NetworkUtils.GetRemoteIp(_httpContext);

                if (request.ProfileImageFile != null && request.ProfileImageFile.Length > 0)
                {
                    var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                    {
                        DataId = appUser.Id,
                        ModelName = nameof(MavUser),
                        File = request.ProfileImageFile,
                    });
                    if (uploadResponse.IsSuccess)
                        appUser.ProfileImagePath = uploadResponse.Url;
                }
                else if (request.ProfileImagePath.HasValue())
                    appUser.ProfileImagePath = request.ProfileImagePath;

                var result = await _userManager.UpdateAsync(appUser);

                if (!result.Succeeded) throw new Exception(LangManager.Translate("Account.UserNotCreated"));

                if (request.UserRoleIdList != null && request.UserRoleIdList.Any())
                {
                    var currentRoles = await _userManager.GetRolesAsync(appUser);
                    await _userManager.RemoveFromRolesAsync(appUser, currentRoles);

                    var roles = _roleManager.Roles.Where(x => request.UserRoleIdList.Contains(x.Id)).ToList();
                    if (roles != null && roles.Any())
                    {
                        for (int i = 0; i < roles.Count; i++)
                        {
                            await _userManager.AddToRoleAsync(appUser, roles[i].Name);
                        }
                    }
                }

                var spec = new AppUserSpec(request.Id);
                var appUserResponse = await _appUserRepository.GetEntityWithSpec<AppUserResponse>(spec, _mapper);

                return new ApiResponse<AppUserResponse>(appUserResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("AppUser.ControllerTitle")));

            }
            catch (ValidationException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new ValidationException(ex.Errors);
            }
            catch (NotFoundException ex)
            {
                _logger.LogError(ex.Message);
                throw new Exception("404", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllAppUserSpecDTO request)
        {
            try
            {
                if (!request.isAll.HasValue)
                    request.isAll = true;

                var spec = new AppUserSpec(request);
                var specCount = new AppUserSpecCount(request);
                var appUserCount = await _appUserRepository.CountAsync(specCount);
                var appUserResponse = await _appUserRepository.ListWithSpecAsync<BaseDropdownResponse>(spec, _mapper);

                return new ApiResponse<BaseDropdownResponse>(request.PageIndex, request.PageSize, appUserCount, appUserResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<AppUserResponse>> GetCurrentUser()
        {
            try
            {
                var userId = _httpContext.HttpContext.User.GetUserId();
                if (!userId.HasValue)
                    throw new UnauthorizedAccessException();

                var userJson = await _cacheService.GetCacheAsync($"AppUser:{userId.Value}");
                AppUserResponse appUserResponse = null;
                if (userJson.HasValue())
                {
                    var user = JsonSerializer.Deserialize<AppUserResponse>(userJson);
                    if (user != null)
                        appUserResponse = user;
                }

                if (appUserResponse == null)
                    throw new UnauthorizedAccessException();

                return new ApiResponse<AppUserResponse>(appUserResponse, appUserResponse != null, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("401", ex);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<BaseDropdownResponse>> GetRoleDropdownList()
        {

            try
            {
                var roles = _roleManager.Roles.ToList();
                var roleResponse = _mapper.Map<IReadOnlyList<BaseDropdownResponse>>(roles);

                return await Task.FromResult(new ApiResponse<BaseDropdownResponse>(1, roleResponse.Count, roleResponse.Count, roleResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully")));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
