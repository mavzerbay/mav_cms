using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface ILanguageService
    {
        Task<ApiResponse<LanguageResponse>> Create(CreateLanguageDTO request);
        Task<ApiResponse<LanguageResponse>> Update(UpdateLanguageDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<LanguageResponse>> GetById(Guid Id);
        Task<ApiResponse<LanguageResponse>> GetAll(GetAllLanguageSpecDTO request);
        Task<ApiResponse<LanguageResponse>> GetAllLanguage();
        Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllLanguageSpecDTO request);
    }
}
