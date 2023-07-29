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
using MAV.Cms.Domain.Entities;
using MAV.Cms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IBaseRepository<Language> _languageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LanguageService> _logger;
        private readonly ICacheService _cacheService;

        public LanguageService(IUnitOfWork uow, IBaseRepository<Language> languageRepository, IMapper mapper, ILogger<LanguageService> logger, ICacheService cacheService)
        {
            _languageRepository = uow.Repository<Language>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<ApiResponse<LanguageResponse>> Create(CreateLanguageDTO request)
        {
            try
            {
                var language = _mapper.Map<Language>(request);

                CreateLanguageValidator validator = new CreateLanguageValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                await _languageRepository.AddAsync(language);

                await _languageRepository.SaveChangesAsync();

                await _cacheService.RemoveFromCacheAsync("Language");

                var spec = new LanguageSpec(language.Id);
                var languageResponse = await _languageRepository.GetEntityWithSpec<LanguageResponse>(spec, _mapper);
                return new ApiResponse<LanguageResponse>(languageResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("Language.ControllerTitle")));
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
                var language = await _languageRepository.GetByIdAsync(request.Id);
                if (language == null)
                    throw new NotFoundException("Language", request.Id);

                if (request.IsSoftDelete)
                {
                    language.isSoftDelete = true;
                    _languageRepository.Update(language);
                }
                else
                    _languageRepository.Delete(language);

                var result = await _languageRepository.SaveChangesAsync();

                await _cacheService.RemoveFromCacheAsync("Language");

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("Language.ControllerTitle")));
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

        public async Task<ApiResponse<LanguageResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new LanguageSpec(Id);
                var languageResponse = await _languageRepository.GetEntityWithSpec<LanguageResponse>(spec, _mapper);
                if (languageResponse == null)
                    throw new NotFoundException("Language", Id);

                return new ApiResponse<LanguageResponse>(languageResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<LanguageResponse>> GetAll(GetAllLanguageSpecDTO request)
        {
            try
            {
                var spec = new LanguageSpec(request);
                var specCount = new LanguageSpecCount(request);
                var languageCount = await _languageRepository.CountAsync(specCount);
                var languageResponse = await _languageRepository.ListWithSpecAsync<LanguageResponse>(spec, _mapper);

                return new ApiResponse<LanguageResponse>(request.PageIndex, request.PageSize, languageCount, languageResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<LanguageResponse>> Update(UpdateLanguageDTO request)
        {
            try
            {
                var spec = new LanguageSpec(request.Id);
                var language = await _languageRepository.GetEntityWithSpec(spec);
                if (language == null)
                    throw new NotFoundException("Language", request.Id);

                _mapper.Map(request, language, typeof(UpdateLanguageDTO), typeof(Language));

                UpdateLanguageValidator validator = new UpdateLanguageValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                _languageRepository.Update(language);

                await _languageRepository.SaveChangesAsync();

                await _cacheService.RemoveFromCacheAsync("Language");

                var languageResponse = await _languageRepository.GetEntityWithSpec<LanguageResponse>(spec, _mapper);

                return new ApiResponse<LanguageResponse>(languageResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Language.ControllerTitle")));
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

        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllLanguageSpecDTO request)
        {
            try
            {
                if (!request.isAll.HasValue)
                    request.isAll = true;

                if (!request.Activity.HasValue)
                    request.Activity = true;

                var spec = new LanguageSpec(request);
                var specCount = new LanguageSpecCount(request);
                var languageCount = await _languageRepository.CountAsync(specCount);
                var languageResponse = await _languageRepository.ListWithSpecAsync<BaseDropdownResponse>(spec, _mapper);

                return new ApiResponse<BaseDropdownResponse>(request.PageIndex, request.PageSize, languageCount, languageResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<LanguageResponse>> GetAllLanguage()
        {
            try
            {
                var languageJson = await _cacheService.GetCacheAsync("Language");

                IReadOnlyList<LanguageResponse> languageResponse = null;
                if (languageJson.HasValue())
                {
                    languageResponse = JsonSerializer.Deserialize<IReadOnlyList<LanguageResponse>>(languageJson);
                }
                if (languageResponse == null || !languageResponse.Any())
                {
                    var spec = new LanguageSpec(new GetAllLanguageSpecDTO() { isAll = true });
                    languageResponse = await _languageRepository.ListWithSpecAsync<LanguageResponse>(spec, _mapper);
                    await _cacheService.SetCacheAsync("Language", languageResponse, TimeSpan.FromDays(30));
                }


                return new ApiResponse<LanguageResponse>(1, languageResponse.Count, languageResponse.Count, languageResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
