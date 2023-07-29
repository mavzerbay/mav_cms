
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
    public class CustomVarController : BaseApiController
    {
        private readonly ICustomVarService _customVarService;

        public CustomVarController(ICustomVarService customVarService)
        {
            _customVarService = customVarService ?? throw new ArgumentNullException(nameof(customVarService));
        }

        [HttpGet("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<CustomVarResponse>> GetCustomVarById(Guid Id) => await _customVarService.GetById(Id);

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<CustomVarResponse>> GetAllCustomVar([FromQuery] GetAllCustomVarSpecDTO query) => await _customVarService.GetAll(query);

        [HttpGet("GetAllCustomVar")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<CustomVarResponse>> GetAllCustomVar() => await _customVarService.GetAllCustomVar();

        [HttpGet("GetDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList([FromQuery] GetAllCustomVarSpecDTO query) => await _customVarService.GetDropdownList(query);

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<CustomVarResponse>> CreateCustomVar([FromBody] CreateCustomVarDTO command) => await _customVarService.Create(command);

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<CustomVarResponse>> UpdateCustomVar([FromBody] UpdateCustomVarDTO command) => await _customVarService.Update(command);

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<bool>> DeleteCustomVar(Guid Id) => await _customVarService.Delete(new BaseDeleteDTO(Id));

    }
}
