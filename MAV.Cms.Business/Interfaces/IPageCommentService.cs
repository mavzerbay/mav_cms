using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IPageCommentService
    {
        Task<ApiResponse<PageCommentResponse>> Create(CreatePageCommentDTO request);
        Task<ApiResponse<PageCommentResponse>> Update(UpdatePageCommentDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<PageCommentResponse>> GetById(Guid Id);
        Task<ApiResponse<PageCommentResponse>> GetAll(GetAllPageCommentSpecDTO request);
    }
}
