
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MAV.Lister.API.Controller
{
    public class MenuController : BaseApiController
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService ?? throw new ArgumentNullException(nameof(menuService));
        }

        [HttpGet("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<MenuResponse>> GetMenuById(Guid Id) => await _menuService.GetById(Id);

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<MenuResponse>> GetAllMenu([FromQuery] GetAllMenuSpecDTO query) => await _menuService.GetAll(query);

        [HttpGet("GetDropdownList")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList([FromQuery] GetAllMenuSpecDTO query) => await _menuService.GetDropdownList(query);

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<MenuResponse>> CreateMenu([FromForm] CreateMenuDTO command) => await _menuService.Create(command);

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<MenuResponse>> UpdateMenu([FromForm] UpdateMenuDTO command) => await _menuService.Update(command);

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<bool>> DeleteMenu(Guid Id) => await _menuService.Delete(new BaseDeleteDTO(Id));

        [HttpGet("AdminMenus")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<ClientMenuResponse>> GetAdminMenus() => await _menuService.GetClientMenuList(new GetAllMenuSpecDTO() { IsBackend = true });

        [HttpGet("ClientMenus/{position:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<ClientMenuResponse>> GetClientMenus(int position = 0) => await _menuService.GetClientMenuList(new GetAllMenuSpecDTO() { IsBackend = false, Position = position });

    }
}
