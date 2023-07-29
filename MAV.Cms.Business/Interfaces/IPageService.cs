using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IPageService
    {
        Task<ApiResponse<PageResponse>> Create(CreatePageDTO request);
        Task<ApiResponse<PageResponse>> Update(UpdatePageDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<PageResponse>> GetById(Guid Id);
        Task<ApiResponse<ClientPageResponse>> GetBySlug(string Slug);
        Task<ApiResponse<PageResponse>> GetAll(GetAllPageSpecDTO request);
        Task<ApiResponse<ClientPageResponse>> GetLatest(GetAllPageSpecDTO request);
        Task<ApiResponse<ClientPageResponse>> GetLatestBlogs();
        Task<ApiResponse<PageDropdownResponse>> GetDropdownList(GetAllPageSpecDTO request);
    }
}
