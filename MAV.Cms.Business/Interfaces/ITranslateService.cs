using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Common.Response;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface ITranslateService
    {
        Task<ApiResponse<TranslateResponse>> Create(CreateTranslateDTO request);
        Task<ApiResponse<TranslateResponse>> Update(UpdateTranslateDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<TranslateResponse>> GetById(Guid Id);
        Task<ApiResponse<TranslateResponse>> GetAll(GetAllTranslateSpecDTO request);
        Task<ApiResponse<TranslationResponse>> GetTranslations();
    }
}
