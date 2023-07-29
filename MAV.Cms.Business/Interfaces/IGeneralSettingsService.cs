using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.Helpers;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IGeneralSettingsService
    {
        Task<ApiResponse<GeneralSettingsResponse>> CreateOrUpdate(CreateOrUpdateGeneralSettingsDTO request);
        Task<ApiResponse<GeneralSettingsResponse>> Get();
    }
}
