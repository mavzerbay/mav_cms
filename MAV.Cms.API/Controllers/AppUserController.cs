
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
    public class AppUserController : BaseApiController
    {
        private readonly IAppUserService _appUserService;

        public AppUserController(IAppUserService appUserService)
        {
            _appUserService = appUserService ?? throw new ArgumentNullException(nameof(appUserService));
        }

        [HttpGet("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<AppUserResponse>> GetAppUserById(Guid Id) => await _appUserService.GetById(Id);

        [HttpGet("CurrentUser")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<AppUserResponse>> GetCurrentUser() => await _appUserService.GetCurrentUser();

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<AppUserResponse>> GetAllAppUser([FromQuery] GetAllAppUserSpecDTO query) => await _appUserService.GetAll(query);

        [HttpGet("GetDropdownList")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList([FromQuery] GetAllAppUserSpecDTO query) => await _appUserService.GetDropdownList(query);

        [HttpGet("GetRoleDropdownList")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<BaseDropdownResponse>> GetRoleDropdownList() => await _appUserService.GetRoleDropdownList();

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<AppUserResponse>> CreateAppUser([FromBody] CreateAppUserDTO command) => await _appUserService.Create(command);

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<AppUserResponse>> UpdateAppUser([FromBody] UpdateAppUserDTO command) => await _appUserService.Update(command);

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<bool>> DeleteAppUser(Guid Id) => await _appUserService.Delete(new BaseDeleteDTO(Id));

    }
}
