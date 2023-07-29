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
using MAV.Cms.Domain.Entities;
using MAV.Cms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class SlideService : ISlideService
    {
        private readonly IBaseRepository<Slide> _slideRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SlideService> _logger;
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _config;
        private readonly IGeneralSettingsService _generalSettingsService;
        public SlideService(IBaseRepository<Slide> slideRepository, IMapper mapper, ILogger<SlideService> logger, IUploadService uploadService, IConfiguration config, IGeneralSettingsService generalSettingsService)
        {
            _slideRepository = slideRepository ?? throw new ArgumentNullException(nameof(slideRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
            _config = config ?? throw new ArgumentNullException(nameof(config));
            _generalSettingsService = generalSettingsService ?? throw new ArgumentNullException(nameof(generalSettingsService));
        }

        public async Task<ApiResponse<SlideResponse>> Create(CreateSlideDTO request)
        {
            try
            {
                CreateSlideValidator validator = new CreateSlideValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                Guid slideId = Guid.NewGuid();

                if (request.SlideMedias != null && request.SlideMedias.Any())
                {
                    for (int i = 0; i < request.SlideMedias.Count; i++)
                    {
                        if (request.SlideMedias.ElementAt(i).BackgroundImageFile != null && request.SlideMedias.ElementAt(i).BackgroundImageFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = slideId,
                                ModelName = nameof(Slide),
                                File = request.SlideMedias.ElementAt(i).BackgroundImageFile,
                            });
                            if (uploadResponse.IsSuccess)
                                request.SlideMedias.ElementAt(i).BackgroundImagePath = uploadResponse.Url;
                        }
                        else
                            request.SlideMedias.ElementAt(i).BackgroundImagePath = null;
                    }
                }

                var slide = _mapper.Map<Slide>(request);
                slide.Id = slideId;
                slide.FillChildMasterId();

                await _slideRepository.AddAsync(slide);

                await _slideRepository.SaveChangesAsync();

                var spec = new SlideSpec(slide.Id, null);
                var slideResponse = await _slideRepository.GetEntityWithSpec<SlideResponse>(spec, _mapper);
                return new ApiResponse<SlideResponse>(slideResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("Slide.ControllerTitle")));
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
                var slide = await _slideRepository.GetByIdAsync(request.Id);
                if (slide == null)
                    throw new NotFoundException("Slide", request.Id);

                if (request.IsSoftDelete)
                {
                    slide.SetModelAndChildSoftDelete();
                    _slideRepository.Update(slide);
                }
                else
                    _slideRepository.Delete(slide);

                var result = await _slideRepository.SaveChangesAsync();

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("Slide.ControllerTitle")));
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

        public async Task<ApiResponse<SlideResponse>> GetById(Guid Id, Guid? LanguageId = null)
        {
            try
            {

                var spec = new SlideSpec(Id, LanguageId, false);
                var slideResponse = await _slideRepository.GetEntityWithSpec<SlideResponse>(spec, _mapper);
                if (slideResponse == null)
                    throw new NotFoundException("Slide", Id);

                if (slideResponse.SlideMedias != null && slideResponse.SlideMedias.Any())
                {
                    for (int i = 0; i < slideResponse.SlideMedias.Count; i++)
                    {
                        slideResponse.SlideMedias.ElementAt(i).BackgroundImagePath = slideResponse.SlideMedias.ElementAt(i).BackgroundImagePath.ResolveUrl(_config);
                    }
                }

                return new ApiResponse<SlideResponse>(slideResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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
        public async Task<ApiResponse<SlideResponse>> GetByPageId(Guid Id)
        {
            try
            {

                var spec = new SlideSpec(Id, LangManager.CurrentLanguageId, true);
                var slideResponse = await _slideRepository.GetEntityWithSpec<SlideResponse>(spec, _mapper);
                if (slideResponse == null)
                    throw new NotFoundException("Slide", Id);

                if (slideResponse.SlideMedias != null && slideResponse.SlideMedias.Any())
                {
                    for (int i = 0; i < slideResponse.SlideMedias.Count; i++)
                    {
                        slideResponse.SlideMedias.ElementAt(i).BackgroundImagePath = slideResponse.SlideMedias.ElementAt(i).BackgroundImagePath.ResolveUrl(_config);
                    }
                }

                return new ApiResponse<SlideResponse>(slideResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<SlideResponse>> GetAll(GetAllSlideSpecDTO request)
        {
            try
            {

                var spec = new SlideSpec(request);
                var specCount = new SlideSpecCount(request);
                var slideCount = await _slideRepository.CountAsync(specCount);
                var slideResponse = await _slideRepository.ListWithSpecAsync<SlideResponse>(spec, _mapper);

                return new ApiResponse<SlideResponse>(request.PageIndex, request.PageSize, slideCount, slideResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<SlideResponse>> Update(UpdateSlideDTO request)
        {
            try
            {
                var spec = new SlideSpec(request.Id, null);
                var slide = await _slideRepository.GetEntityWithSpec(spec);
                if (slide == null)
                    throw new NotFoundException("Slide", request.Id);

                UpdateSlideValidator validator = new UpdateSlideValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                slide.FillChildMasterId();

                if (request.SlideMedias != null && request.SlideMedias.Any())
                {
                    for (int i = 0; i < request.SlideMedias.Count; i++)
                    {
                        if (request.SlideMedias.ElementAt(i).BackgroundImageFile != null && request.SlideMedias.ElementAt(i).BackgroundImageFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = slide.Id,
                                ModelName = nameof(Slide),
                                File = request.SlideMedias.ElementAt(i).BackgroundImageFile,
                            });
                            if (uploadResponse.IsSuccess)
                                request.SlideMedias.ElementAt(i).BackgroundImagePath = uploadResponse.Url;
                        }
                    }
                }

                _mapper.Map(request, slide, typeof(UpdateSlideDTO), typeof(Slide));

                _slideRepository.Update(slide);

                await _slideRepository.SaveChangesAsync();

                var slideResponse = await _slideRepository.GetEntityWithSpec<SlideResponse>(spec, _mapper);

                return new ApiResponse<SlideResponse>(slideResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Slide.ControllerTitle")));
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

        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllSlideSpecDTO request)
        {
            try
            {
                if (!request.isAll.HasValue)
                    request.isAll = true;

                if (!request.Activity.HasValue)
                    request.Activity = true;

                var spec = new SlideSpec(request);
                var specCount = new SlideSpecCount(request);
                var slideCount = await _slideRepository.CountAsync(specCount);
                var slideResponse = await _slideRepository.ListWithSpecAsync<BaseDropdownResponse>(spec, _mapper);

                return new ApiResponse<BaseDropdownResponse>(request.PageIndex, request.PageSize, slideCount, slideResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<SlideResponse>> GetHomeSlide()
        {
            try
            {

                var spec = new SlideSpec(new GetAllSlideSpecDTO() { Activity = true, isHome = true, PageSize = 1, LanguageId = LangManager.CurrentLanguageId });
                var slideResponseList = await _slideRepository.ListWithSpecAsync<SlideResponse>(spec, _mapper);
                SlideResponse slideResponse = null;
                if (slideResponseList != null && slideResponseList.Any())
                {
                    slideResponse = slideResponseList.FirstOrDefault();
                    for (int i = 0; i < slideResponse.SlideMedias.Count; i++)
                    {
                        slideResponse.SlideMedias.ElementAt(i).BackgroundImagePath = slideResponse.SlideMedias.ElementAt(i).BackgroundImagePath.ResolveUrl(_config);
                    }
                }

                return new ApiResponse<SlideResponse>(slideResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Slide.ControllerTitle")));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<SlideResponse>> GetTestimonialSlide()
        {
            try
            {
                var generalSettingsApiResponse = await _generalSettingsService.Get();
                GeneralSettingsResponse generalSettingsResponse = null;
                if (generalSettingsApiResponse.IsSuccess)
                {
                    generalSettingsResponse = generalSettingsApiResponse.DataSingle;
                }
                SlideResponse slideResponse = null;
                if (generalSettingsResponse != null && !generalSettingsResponse.TestimonialSlideId.IsNullOrEmpty())
                {
                    var slideApiResponse = await GetById(generalSettingsResponse.TestimonialSlideId.Value, LangManager.CurrentLanguageId);
                    if (slideApiResponse.IsSuccess)
                        slideResponse = slideApiResponse.DataSingle;
                }

                return new ApiResponse<SlideResponse>(slideResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Slide.ControllerTitle")));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
