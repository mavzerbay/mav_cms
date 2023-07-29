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
    public class CustomVarService : ICustomVarService
    {
        private readonly IBaseRepository<CustomVar> _customVarRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomVarService> _logger;
        private readonly ICacheService _cacheService;

        public CustomVarService(IUnitOfWork uow, IBaseRepository<CustomVar> customVarRepository, IMapper mapper, ILogger<CustomVarService> logger, ICacheService cacheService)
        {
            _customVarRepository = uow.Repository<CustomVar>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _cacheService = cacheService ?? throw new ArgumentNullException(nameof(cacheService));
        }

        public async Task<ApiResponse<CustomVarResponse>> Create(CreateCustomVarDTO request)
        {
            try
            {
                var customVar = _mapper.Map<CustomVar>(request);

                CreateCustomVarValidator validator = new CreateCustomVarValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                customVar.FillChildMasterId();

                await _customVarRepository.AddAsync(customVar);

                await _customVarRepository.SaveChangesAsync();

                await _cacheService.RemoveFromCacheAsync("CustomVar");

                var spec = new CustomVarSpec(customVar.Id, LangManager.CurrentLanguageId);
                var customVarResponse = await _customVarRepository.GetEntityWithSpec<CustomVarResponse>(spec, _mapper);
                return new ApiResponse<CustomVarResponse>(customVarResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("CustomVar.ControllerTitle")));
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
            try
            {
                var customVar = await _customVarRepository.GetByIdAsync(request.Id);
                if (customVar == null)
                    throw new NotFoundException("CustomVar", request.Id);

                if (request.IsSoftDelete)
                {
                    customVar.SetModelAndChildSoftDelete();
                    _customVarRepository.Update(customVar);
                }
                else
                    _customVarRepository.Delete(customVar);

                var result = await _customVarRepository.SaveChangesAsync();

                await _cacheService.RemoveFromCacheAsync("CustomVar");

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("CustomVar.ControllerTitle")));
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

        public async Task<ApiResponse<CustomVarResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new CustomVarSpec(Id);
                var customVarResponse = await _customVarRepository.GetEntityWithSpec<CustomVarResponse>(spec, _mapper);
                if (customVarResponse == null)
                    throw new NotFoundException("CustomVar", Id);

                return new ApiResponse<CustomVarResponse>(customVarResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<CustomVarResponse>> GetAll(GetAllCustomVarSpecDTO request)
        {
            try
            {
                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                var spec = new CustomVarSpec(request);
                var specCount = new CustomVarSpecCount(request);
                var customVarCount = await _customVarRepository.CountAsync(specCount);
                var customVarResponse = await _customVarRepository.ListWithSpecAsync<CustomVarResponse>(spec, _mapper);

                return new ApiResponse<CustomVarResponse>(request.PageIndex, request.PageSize, customVarCount, customVarResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<CustomVarResponse>> Update(UpdateCustomVarDTO request)
        {
            try
            {
                var spec = new CustomVarSpec(request.Id);
                var customVar = await _customVarRepository.GetEntityWithSpec(spec);
                if (customVar == null)
                    throw new NotFoundException("CustomVar", request.Id);

                _mapper.Map(request, customVar, typeof(UpdateCustomVarDTO), typeof(CustomVar));

                UpdateCustomVarValidator validator = new UpdateCustomVarValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                customVar.FillChildMasterId();

                _customVarRepository.Update(customVar);

                await _customVarRepository.SaveChangesAsync();

                await _cacheService.RemoveFromCacheAsync("CustomVar");

                var customVarResponse = await _customVarRepository.GetEntityWithSpec<CustomVarResponse>(spec, _mapper);

                return new ApiResponse<CustomVarResponse>(customVarResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("CustomVar.ControllerTitle")));
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

        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllCustomVarSpecDTO request)
        {
            try
            {
                if (!request.isAll.HasValue)
                    request.isAll = true;

                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                var spec = new CustomVarSpec(request);
                var specCount = new CustomVarSpecCount(request);
                var customVarCount = await _customVarRepository.CountAsync(specCount);
                var customVarResponse = await _customVarRepository.ListWithSpecAsync<BaseDropdownResponse>(spec, _mapper);

                return new ApiResponse<BaseDropdownResponse>(request.PageIndex, request.PageSize, customVarCount, customVarResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<CustomVarResponse>> GetAllCustomVar(string GroupName)
        {
            try
            {
                var customVarJson = await _cacheService.GetCacheAsync("CustomVar");

                IReadOnlyList<CustomVarResponse> customVarResponse = null;
                if (customVarJson.HasValue())
                {
                    customVarResponse = JsonSerializer.Deserialize<IReadOnlyList<CustomVarResponse>>(customVarJson);
                }
                if (customVarResponse == null || !customVarResponse.Any())
                {
                    var spec = new CustomVarSpec(new GetAllCustomVarSpecDTO() { isAll = true });
                    customVarResponse = await _customVarRepository.ListWithSpecAsync<CustomVarResponse>(spec, _mapper);
                    await _cacheService.SetCacheAsync("CustomVar", customVarResponse, TimeSpan.FromDays(30));
                }

                if (GroupName.HasValue())
                    customVarResponse = customVarResponse.Where(x => x.GroupName == GroupName).ToList();


                return new ApiResponse<CustomVarResponse>(1, customVarResponse.Count, customVarResponse.Count, customVarResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
