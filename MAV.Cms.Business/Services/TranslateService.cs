using AutoMapper;
using FluentValidation.Results;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Exceptions;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Business.Specifications;
using MAV.Cms.Business.Validators;
using MAV.Cms.Common.Extensions;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Common.Interfaces;
using MAV.Cms.Common.Response;
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
    public class TranslateService : ITranslateService
    {
        private readonly IBaseRepository<Translate> _translateRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<TranslateService> _logger;
        private readonly ICacheService _cacheService;

        public TranslateService(IUnitOfWork uow, IBaseRepository<Translate> translateRepository, IMapper mapper, ILogger<TranslateService> logger, ICacheService cacheService)
        {
            _translateRepository = uow.Repository<Translate>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<ApiResponse<TranslateResponse>> Create(CreateTranslateDTO request)
        {
            try
            {
                var translate = _mapper.Map<Translate>(request);

                CreateTranslateValidator validator = new CreateTranslateValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                await _translateRepository.AddAsync(translate);

                await _translateRepository.SaveChangesAsync();


                var keys = _cacheService.GetKeysByPattern("Translation");

                for (int i = 0; i < keys.Count; i++)
                {
                    await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));

                }

                var spec = new TranslateSpec(translate.Id);
                var translateResponse = await _translateRepository.GetEntityWithSpec<TranslateResponse>(spec, _mapper);
                return new ApiResponse<TranslateResponse>(translateResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("Translate.ControllerTitle")));
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
                var translate = await _translateRepository.GetByIdAsync(request.Id);
                if (translate == null)
                    throw new NotFoundException("Translate", request.Id);

                if (request.IsSoftDelete)
                {
                    translate.isSoftDelete = true;
                    _translateRepository.Update(translate);
                }
                else
                    _translateRepository.Delete(translate);

                var result = await _translateRepository.SaveChangesAsync();

                var keys = _cacheService.GetKeysByPattern("Translation");

                for (int i = 0; i < keys.Count; i++)
                {
                    await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));

                }

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("Translate.ControllerTitle")));
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

        public async Task<ApiResponse<TranslateResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new TranslateSpec(Id);
                var translateResponse = await _translateRepository.GetEntityWithSpec<TranslateResponse>(spec, _mapper);
                if (translateResponse == null)
                    throw new NotFoundException("Translate", Id);

                return new ApiResponse<TranslateResponse>(translateResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<TranslateResponse>> GetAll(GetAllTranslateSpecDTO request)
        {
            try
            {
                var spec = new TranslateSpec(request);
                var specCount = new TranslateSpecCount(request);
                var translateCount = await _translateRepository.CountAsync(specCount);
                var translateResponse = await _translateRepository.ListWithSpecAsync<TranslateResponse>(spec, _mapper);

                return new ApiResponse<TranslateResponse>(request.PageIndex, request.PageSize, translateCount, translateResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<TranslateResponse>> Update(UpdateTranslateDTO request)
        {
            try
            {
                var spec = new TranslateSpec(request.Id);
                var translate = await _translateRepository.GetEntityWithSpec(spec);
                if (translate == null)
                    throw new NotFoundException("Translate", request.Id);

                _mapper.Map(request, translate, typeof(UpdateTranslateDTO), typeof(Translate));

                UpdateTranslateValidator validator = new UpdateTranslateValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                _translateRepository.Update(translate);

                await _translateRepository.SaveChangesAsync();

                var keys = _cacheService.GetKeysByPattern("Translation");

                for (int i = 0; i < keys.Count; i++)
                {
                    await _cacheService.RemoveFromCacheAsync(keys.ElementAt(i));

                }

                var translateResponse = await _translateRepository.GetEntityWithSpec<TranslateResponse>(spec, _mapper);

                return new ApiResponse<TranslateResponse>(translateResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Translate.ControllerTitle")));
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

        public async Task<ApiResponse<TranslationResponse>> GetTranslations()
        {
            try
            {
                Guid currentLanguageId = LangManager.CurrentLanguageId;
                var translationResponse = await _cacheService.GetCacheAsync($"Translation:{currentLanguageId}");

                IReadOnlyList<TranslationResponse> translateResponse = null;
                if (translationResponse.HasValue())
                {
                    var translationList = JsonSerializer.Deserialize<IReadOnlyList<TranslationResponse>>(translationResponse);
                    if (translationList != null && translationList.Any())
                        translateResponse = translationList;
                }
                else
                {
                    var spec = new TranslateSpec(new GetAllTranslateSpecDTO() { isAll = true, LanguageId = currentLanguageId });
                    translateResponse = await _translateRepository.ListWithSpecAsync<TranslationResponse>(spec, _mapper);
                    if (translateResponse != null && translateResponse.Any())
                        await _cacheService.SetCacheAsync($"Translation:{currentLanguageId}", translateResponse, TimeSpan.FromDays(30));

                }


                return new ApiResponse<TranslationResponse>(0, 0, 0, translateResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
