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
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IBaseRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(IUnitOfWork uow, IMapper mapper, ILogger<CategoryService> logger)
        {
            _categoryRepository = uow.Repository<Category>();
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<ApiResponse<CategoryResponse>> Create(CreateCategoryDTO request)
        {
            try
            {
                var category = _mapper.Map<Category>(request);

                CreateCategoryValidator validator = new CreateCategoryValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                category.FillChildMasterId();

                await _categoryRepository.AddAsync(category);

                await _categoryRepository.SaveChangesAsync();

                var spec = new CategorySpec(category.Id);
                var categoryResponse = await _categoryRepository.GetEntityWithSpec<CategoryResponse>(spec, _mapper);
                return new ApiResponse<CategoryResponse>(categoryResponse, true, StatusCodes.Status201Created, string.Format(LangManager.Translate("Common.Created"), LangManager.Translate("Category.ControllerTitle")));
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
                var category = await _categoryRepository.GetByIdAsync(request.Id);
                if (category == null)
                    throw new NotFoundException("Category", request.Id);

                if (request.IsSoftDelete)
                {
                    category.SetModelAndChildSoftDelete();
                    _categoryRepository.Update(category);
                }
                else
                    _categoryRepository.Delete(category);

                var result = await _categoryRepository.SaveChangesAsync();

                return new ApiResponse<bool>(result > 0, true, StatusCodes.Status200OK, string.Format(LangManager.Translate("Common.Deleted"), LangManager.Translate("Category.ControllerTitle")));
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

        public async Task<ApiResponse<CategoryResponse>> GetById(Guid Id)
        {
            try
            {

                var spec = new CategorySpec(Id);
                var categoryResponse = await _categoryRepository.GetEntityWithSpec<CategoryResponse>(spec, _mapper);
                if (categoryResponse == null)
                    throw new NotFoundException("Category", Id);

                return new ApiResponse<CategoryResponse>(categoryResponse, true, StatusCodes.Status200OK, LangManager.Translate("Common.DataReceivedSuccessfully"));
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

        public async Task<ApiResponse<CategoryResponse>> GetAll(GetAllCategorySpecDTO request)
        {
            try
            {

                var spec = new CategorySpec(request);
                var specCount = new CategorySpecCount(request);
                var categoryCount = await _categoryRepository.CountAsync(specCount);
                var categoryResponse = await _categoryRepository.ListWithSpecAsync<CategoryResponse>(spec, _mapper);

                return new ApiResponse<CategoryResponse>(request.PageIndex, request.PageSize, categoryCount, categoryResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }

        public async Task<ApiResponse<CategoryResponse>> Update(UpdateCategoryDTO request)
        {
            try
            {
                var spec = new CategorySpec(request.Id);
                var category = await _categoryRepository.GetEntityWithSpec(spec);
                if (category == null)
                    throw new NotFoundException("Category", request.Id);

                _mapper.Map(request, category, typeof(UpdateCategoryDTO), typeof(Category));

                UpdateCategoryValidator validator = new UpdateCategoryValidator();
                ValidationResult validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }

                category.FillChildMasterId();

                _categoryRepository.Update(category);

                await _categoryRepository.SaveChangesAsync();

                var categoryResponse = await _categoryRepository.GetEntityWithSpec<CategoryResponse>(spec, _mapper);

                return new ApiResponse<CategoryResponse>(categoryResponse, true, StatusCodes.Status204NoContent, string.Format(LangManager.Translate("Common.Updated"), LangManager.Translate("Category.ControllerTitle")));
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

        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllCategorySpecDTO request)
        {
            try
            {
                if (!request.isAll.HasValue)
                    request.isAll = true;

                if (!request.Activity.HasValue)
                    request.Activity = true;

                var spec = new CategorySpec(request);
                var specCount = new CategorySpecCount(request);
                var categoryCount = await _categoryRepository.CountAsync(specCount);
                var categoryResponse = await _categoryRepository.ListWithSpecAsync<BaseDropdownResponse>(spec, _mapper);

                return new ApiResponse<BaseDropdownResponse>(request.PageIndex, request.PageSize, categoryCount, categoryResponse, StatusCodes.Status200OK, true, LangManager.Translate("Common.DataReceivedSuccessfully"));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                throw new Exception("400", ex);
            }
        }
    }
}
