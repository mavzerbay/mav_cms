using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface ISlideService
    {
        Task<ApiResponse<SlideResponse>> Create(CreateSlideDTO request);
        Task<ApiResponse<SlideResponse>> Update(UpdateSlideDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<SlideResponse>> GetById(Guid Id, Guid? LanguageId = null);
        Task<ApiResponse<SlideResponse>> GetByPageId(Guid Id);
        Task<ApiResponse<SlideResponse>> GetAll(GetAllSlideSpecDTO request);
        Task<ApiResponse<SlideResponse>> GetHomeSlide();
        Task<ApiResponse<SlideResponse>> GetTestimonialSlide();
        Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllSlideSpecDTO request);
    }
}
