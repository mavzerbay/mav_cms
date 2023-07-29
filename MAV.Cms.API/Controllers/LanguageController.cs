
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
    public class LanguageController : BaseApiController
    {
        private readonly ILanguageService _languageService;

        public LanguageController(ILanguageService languageService)
        {
            _languageService = languageService ?? throw new ArgumentNullException(nameof(languageService));
        }

        [HttpGet("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<LanguageResponse>> GetLanguageById(Guid Id) => await _languageService.GetById(Id);

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<LanguageResponse>> GetAllLanguage([FromQuery] GetAllLanguageSpecDTO query) => await _languageService.GetAll(query);

        [HttpGet("GetAllLanguage")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<LanguageResponse>> GetAllLanguage() => await _languageService.GetAllLanguage();

        [HttpGet("GetDropdownList")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList([FromQuery] GetAllLanguageSpecDTO query) => await _languageService.GetDropdownList(query);

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<LanguageResponse>> CreateLanguage([FromBody] CreateLanguageDTO command) => await _languageService.Create(command);

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<LanguageResponse>> UpdateLanguage([FromBody] UpdateLanguageDTO command) => await _languageService.Update(command);

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<bool>> DeleteLanguage(Guid Id) => await _languageService.Delete(new BaseDeleteDTO(Id));

    }
}
