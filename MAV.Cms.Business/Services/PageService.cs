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
    public class PageService : IPageService
    {
        private readonly IBaseRepository<Page> _pageRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PageService> _logger;
        private readonly IUploadService _uploadService;
        private readonly IConfiguration _config;

        public PageService(IUnitOfWork uow, IMapper mapper, ILogger<PageService> logger, IUploadService uploadService, IConfiguration config)
        {
            _pageRepository = uow.Repository<Page>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _uploadService = uploadService ?? throw new ArgumentNullException(nameof(uploadService));
            _config = config ?? throw new ArgumentNullException(nameof(config));
        }

        public async Task<ApiResponse<PageResponse>> Create(CreatePageDTO request)
        {
            try
            {
                var page = _mapper.Map<Page>(request);

                CreatePageValidator validator = new CreatePageValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                page.FillChildMasterId();

                if (request.PageTrans != null && request.PageTrans.Any())
                {
                    for (int i = 0; i < request.PageTrans.Count; i++)
                    {
                        if (request.PageTrans.ElementAt(i).HeaderFile != null && request.PageTrans.ElementAt(i).HeaderFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = page.Id,
                                ModelName = nameof(Page),
                                File = request.PageTrans.ElementAt(i).HeaderFile,
                            });
                            if (uploadResponse.IsSuccess)
                                page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).HeaderPath = uploadResponse.Url;
                        }
                        if (request.PageTrans.ElementAt(i).BackgroundFile != null && request.PageTrans.ElementAt(i).BackgroundFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = page.Id,
                                ModelName = nameof(Page),
                                File = request.PageTrans.ElementAt(i).BackgroundFile,
                            });
                            if (uploadResponse.IsSuccess)
                                page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).BackgroundPath = uploadResponse.Url;
                        }
                        if (request.PageTrans.ElementAt(i).OgImageFile != null && request.PageTrans.ElementAt(i).OgImageFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = page.Id,
                                ModelName = nameof(Page),
                                File = request.PageTrans.ElementAt(i).OgImageFile,
                            });
                            if (uploadResponse.IsSuccess)
                                page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).OgImagePath = uploadResponse.Url;
                        }
                    }
                }

                await _pageRepository.AddAsync(page);

                await _pageRepository.SaveChangesAsync();

                var spec = new PageSpec(page.Id);
                var pageResponse = await _pageRepository.GetEntityWithSpec<PageResponse>(spec, _mapper);
                return new ApiResponse<PageResponse>(pageResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("Page.ControllerTitle")));
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
                var page = await _pageRepository.GetByIdAsync(request.Id);
                if (page == null)
                    throw new NotFoundException("Page", request.Id);

                if (request.IsSoftDelete)
                {
                    page.SetModelAndChildSoftDelete();
                    _pageRepository.Update(page);
                }
                else
                    _pageRepository.Delete(page);

                var result = await _pageRepository.SaveChangesAsync();

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("Page.ControllerTitle")));
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

        public async Task<ApiResponse<PageResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new PageSpec(Id);
                var pageResponse = await _pageRepository.GetEntityWithSpec<PageResponse>(spec, _mapper);
                if (pageResponse == null)
                    throw new NotFoundException("Page", Id);

                Parallel.ForEach(pageResponse.PageTrans, item =>
                {
                    item.HeaderPath = item.HeaderPath.ResolveUrl(_config);
                    item.BackgroundPath = item.BackgroundPath.ResolveUrl(_config);
                    item.OgImagePath = item.OgImagePath.ResolveUrl(_config);
                });

                return new ApiResponse<PageResponse>(pageResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<PageResponse>> GetAll(GetAllPageSpecDTO request)
        {
            try
            {
                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                var spec = new PageSpec(request);
                var specCount = new PageSpecCount(request);
                var pageCount = await _pageRepository.CountAsync(specCount);
                var pageResponse = await _pageRepository.ListWithSpecAsync<PageResponse>(spec, _mapper);

                return new ApiResponse<PageResponse>(request.PageIndex, request.PageSize, pageCount, pageResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<PageResponse>> Update(UpdatePageDTO request)
        {
            try
            {
                var spec = new PageSpec(request.Id);
                var page = await _pageRepository.GetEntityWithSpec(spec);
                if (page == null)
                    throw new NotFoundException("Page", request.Id);

                _mapper.Map(request, page, typeof(UpdatePageDTO), typeof(Page));

                UpdatePageValidator validator = new UpdatePageValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                page.FillChildMasterId();

                if (request.PageTrans != null && request.PageTrans.Any())
                {
                    for (int i = 0; i < request.PageTrans.Count; i++)
                    {
                        if (request.PageTrans.ElementAt(i).HeaderFile != null && request.PageTrans.ElementAt(i).HeaderFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = page.Id,
                                ModelName = nameof(Page),
                                File = request.PageTrans.ElementAt(i).HeaderFile,
                            });
                            if (uploadResponse.IsSuccess)
                                page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).HeaderPath = uploadResponse.Url;
                        }
                        else if (request.PageTrans.ElementAt(i).HeaderPath.HasValue())
                            page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).HeaderPath = request.PageTrans.ElementAt(i).HeaderPath;

                        if (request.PageTrans.ElementAt(i).BackgroundFile != null && request.PageTrans.ElementAt(i).BackgroundFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = page.Id,
                                ModelName = nameof(Page),
                                File = request.PageTrans.ElementAt(i).BackgroundFile,
                            });
                            if (uploadResponse.IsSuccess)
                                page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).BackgroundPath = uploadResponse.Url;
                        }
                        else if (request.PageTrans.ElementAt(i).BackgroundPath.HasValue())
                            page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).BackgroundPath = request.PageTrans.ElementAt(i).BackgroundPath;


                        if (request.PageTrans.ElementAt(i).OgImageFile != null && request.PageTrans.ElementAt(i).OgImageFile.Length > 0)
                        {
                            var uploadResponse = await _uploadService.UploadAsync(new DTOs.UploadDTOs.CreateUploadDTO
                            {
                                DataId = page.Id,
                                ModelName = nameof(Page),
                                File = request.PageTrans.ElementAt(i).OgImageFile,
                            });
                            if (uploadResponse.IsSuccess)
                                page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).OgImagePath = uploadResponse.Url;
                        }
                        else if (request.PageTrans.ElementAt(i).OgImagePath.HasValue())
                            page.PageTrans.FirstOrDefault(x => x.LanguageId == request.PageTrans.ElementAt(i).LanguageId).OgImagePath = request.PageTrans.ElementAt(i).OgImagePath;
                    }
                }

                _pageRepository.Update(page);

                await _pageRepository.SaveChangesAsync();

                var pageResponse = await _pageRepository.GetEntityWithSpec<PageResponse>(spec, _mapper);

                return new ApiResponse<PageResponse>(pageResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Page.ControllerTitle")));
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

        public async Task<ApiResponse<PageDropdownResponse>> GetDropdownList(GetAllPageSpecDTO request)
        {
            try
            {
                if (!request.isAll.HasValue)
                    request.isAll = true;

                if (!request.Activity.HasValue)
                    request.Activity = true;

                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                var spec = new PageSpec(request);
                var specCount = new PageSpecCount(request);
                var pageCount = await _pageRepository.CountAsync(specCount);
                var pageResponse = await _pageRepository.ListWithSpecAsync<PageDropdownResponse>(spec, _mapper);

                return new ApiResponse<PageDropdownResponse>(request.PageIndex, request.PageSize, pageCount, pageResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<ClientPageResponse>> GetBySlug(string Slug)
        {
            try
            {

                var spec = new PageSpec(Slug);
                var pageResponse = await _pageRepository.GetEntityWithSpec<ClientPageResponse>(spec, _mapper);
                if (pageResponse == null)
                    throw new NotFoundException("Page", Slug);

                pageResponse.HeaderPath = pageResponse.HeaderPath.ResolveUrl(_config);
                pageResponse.BackgroundPath = pageResponse.BackgroundPath.ResolveUrl(_config);
                pageResponse.OgImagePath = pageResponse.OgImagePath.ResolveUrl(_config);

                if (pageResponse.ChildPageList != null && pageResponse.ChildPageList.Any())
                {
                    Parallel.ForEach(pageResponse.ChildPageList, item =>
                    {
                        item.HeaderPath = item.HeaderPath.ResolveUrl(_config);
                        item.BackgroundPath = item.BackgroundPath.ResolveUrl(_config);
                        item.OgImagePath = item.OgImagePath.ResolveUrl(_config);
                    });
                }

                return new ApiResponse<ClientPageResponse>(pageResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<ClientPageResponse>> GetLatest(GetAllPageSpecDTO request)
        {
            try
            {
                if (!request.LanguageId.HasValue)
                    request.LanguageId = LangManager.CurrentLanguageId;

                request.IncludeCreatedBy = true;

                var spec = new PageSpec(request);
                var specCount = new PageSpecCount(request);
                var pageCount = await _pageRepository.CountAsync(specCount);
                var pageResponse = await _pageRepository.ListWithSpecAsync<ClientPageResponse>(spec, _mapper);

                if (pageResponse != null && pageResponse.Any())
                {

                    Parallel.ForEach(pageResponse, item =>
                    {
                        item.HeaderPath = item.HeaderPath.ResolveUrl(_config);
                        item.BackgroundPath = item.BackgroundPath.ResolveUrl(_config);
                        item.OgImagePath = item.OgImagePath.ResolveUrl(_config);
                    });
                }

                return new ApiResponse<ClientPageResponse>(request.PageIndex, request.PageSize, pageCount, pageResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
        public async Task<ApiResponse<ClientPageResponse>> GetLatestBlogs()
        {
            try
            {
                var query = new GetAllPageSpecDTO
                {
                    LanguageId = LangManager.CurrentLanguageId,
                    IncludeCreatedBy = true,
                    PageTypeKeyName = "BlogDetail",
                    PageSize = 4,
                    Sort = "createdDateDesc",
                };

                var spec = new PageSpec(query);
                var specCount = new PageSpecCount(query);
                var pageCount = await _pageRepository.CountAsync(specCount);
                var pageResponse = await _pageRepository.ListWithSpecAsync<ClientPageResponse>(spec, _mapper);

                if (pageResponse != null && pageResponse.Any())
                {

                    Parallel.ForEach(pageResponse, item =>
                    {
                        item.HeaderPath = item.HeaderPath.ResolveUrl(_config);
                        item.BackgroundPath = item.BackgroundPath.ResolveUrl(_config);
                        item.OgImagePath = item.OgImagePath.ResolveUrl(_config);
                    });
                }

                return new ApiResponse<ClientPageResponse>(1, 4, pageCount, pageResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
