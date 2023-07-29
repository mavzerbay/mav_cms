using AutoMapper;
using FluentValidation.Results;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Exceptions;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Business.Specifications;
using MAV.Cms.Business.Validators;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Domain.Entities;
using MAV.Cms.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class PageCommentService : IPageCommentService
    {
        private readonly IBaseRepository<PageComment> _pageCommentRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PageCommentService> _logger;

        public PageCommentService(IUnitOfWork uow, IBaseRepository<PageComment> pageCommentRepository, IMapper mapper, ILogger<PageCommentService> logger)
        {
            _pageCommentRepository = uow.Repository<PageComment>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse<PageCommentResponse>> Create(CreatePageCommentDTO request)
        {
            try
            {
                var pageComment = _mapper.Map<PageComment>(request);

                CreatePageCommentValidator validator = new CreatePageCommentValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                await _pageCommentRepository.AddAsync(pageComment);

                await _pageCommentRepository.SaveChangesAsync();

                var spec = new PageCommentSpec(pageComment.Id);
                var pageCommentResponse = await _pageCommentRepository.GetEntityWithSpec<PageCommentResponse>(spec, _mapper);
                return new ApiResponse<PageCommentResponse>(pageCommentResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("PageComment.ControllerTitle")));
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
                var pageComment = await _pageCommentRepository.GetByIdAsync(request.Id);
                if (pageComment == null)
                    throw new NotFoundException("PageComment", request.Id);

                if (request.IsSoftDelete)
                {
                    pageComment.isSoftDelete = true;
                    _pageCommentRepository.Update(pageComment);
                }
                else
                    _pageCommentRepository.Delete(pageComment);

                var result = await _pageCommentRepository.SaveChangesAsync();

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("PageComment.ControllerTitle")));
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

        public async Task<ApiResponse<PageCommentResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new PageCommentSpec(Id);
                var pageCommentResponse = await _pageCommentRepository.GetEntityWithSpec<PageCommentResponse>(spec, _mapper);
                if (pageCommentResponse == null)
                    throw new NotFoundException("PageComment", Id);

                return new ApiResponse<PageCommentResponse>(pageCommentResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<PageCommentResponse>> GetAll(GetAllPageCommentSpecDTO request)
        {
            try
            {

                var spec = new PageCommentSpec(request);
                var specCount = new PageCommentSpecCount(request);
                var pageCommentCount = await _pageCommentRepository.CountAsync(specCount);
                var pageCommentResponse = await _pageCommentRepository.ListWithSpecAsync<PageCommentResponse>(spec, _mapper);

                return new ApiResponse<PageCommentResponse>(request.PageIndex, request.PageSize, pageCommentCount, pageCommentResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<PageCommentResponse>> Update(UpdatePageCommentDTO request)
        {
            try
            {
                var spec = new PageCommentSpec(request.Id);
                var pageComment = await _pageCommentRepository.GetEntityWithSpec(spec);
                if (pageComment == null)
                    throw new NotFoundException("PageComment", request.Id);

                _mapper.Map(request, pageComment, typeof(UpdatePageCommentDTO), typeof(PageComment));

                UpdatePageCommentValidator validator = new UpdatePageCommentValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                _pageCommentRepository.Update(pageComment);

                await _pageCommentRepository.SaveChangesAsync();

                var pageCommentResponse = await _pageCommentRepository.GetEntityWithSpec<PageCommentResponse>(spec, _mapper);

                return new ApiResponse<PageCommentResponse>(pageCommentResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("PageComment.ControllerTitle")));
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
    }
}
