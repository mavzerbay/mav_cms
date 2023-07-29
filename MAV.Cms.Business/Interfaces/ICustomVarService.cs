using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface ICustomVarService
    {
        Task<ApiResponse<CustomVarResponse>> Create(CreateCustomVarDTO request);
        Task<ApiResponse<CustomVarResponse>> Update(UpdateCustomVarDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<CustomVarResponse>> GetById(Guid Id);
        Task<ApiResponse<CustomVarResponse>> GetAll(GetAllCustomVarSpecDTO request);
        Task<ApiResponse<CustomVarResponse>> GetAllCustomVar(string GroupName = null);
        Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllCustomVarSpecDTO request);
    }
}
