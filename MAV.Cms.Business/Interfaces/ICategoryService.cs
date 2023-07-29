using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<CategoryResponse>> Create(CreateCategoryDTO request);
        Task<ApiResponse<CategoryResponse>> Update(UpdateCategoryDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<CategoryResponse>> GetById(Guid Id);
        Task<ApiResponse<CategoryResponse>> GetAll(GetAllCategorySpecDTO request);
        Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllCategorySpecDTO request);
    }
}
