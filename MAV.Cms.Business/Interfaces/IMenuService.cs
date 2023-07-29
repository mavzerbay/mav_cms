using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface IMenuService
    {
        Task<ApiResponse<MenuResponse>> Create(CreateMenuDTO request);
        Task<ApiResponse<MenuResponse>> Update(UpdateMenuDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<MenuResponse>> GetById(Guid Id);
        Task<ApiResponse<MenuResponse>> GetAll(GetAllMenuSpecDTO request);
        Task<ApiResponse<BaseDropdownResponse>> GetDropdownList(GetAllMenuSpecDTO request);
        Task<ApiResponse<ClientMenuResponse>> GetClientMenuList(GetAllMenuSpecDTO request);
    }
}
