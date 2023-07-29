using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IAppUserService
    {
        Task<ApiResponse<AppUserResponse>> Create(CreateAppUserDTO request);
        Task<ApiResponse<AppUserResponse>> Update(UpdateAppUserDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<AppUserResponse>> GetById(Guid Id);
        Task<ApiResponse<AppUserResponse>> GetAll(GetAllAppUserSpecDTO request);
        Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllAppUserSpecDTO request);
        Task<ApiResponse<BaseDropdownResponse>> GetRoleDropdownList();
        Task<ApiResponse<AppUserResponse>> GetCurrentUser();
    }
}
