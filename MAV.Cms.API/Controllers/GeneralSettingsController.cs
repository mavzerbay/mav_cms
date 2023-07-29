
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
using System.Linq;
using System.Threading.Tasks;

namespace MAV.Lister.API.Controller
{
    public class GeneralSettingsController : BaseApiController
    {
        private readonly IGeneralSettingsService _generalSettingsService;

        public GeneralSettingsController(IGeneralSettingsService generalSettingsService)
        {
            _generalSettingsService = generalSettingsService ?? throw new ArgumentNullException(nameof(generalSettingsService));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<GeneralSettingsResponse>> GetGeneralSettings() => await _generalSettingsService.Get();

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<GeneralSettingsResponse>> CreateOrUpdateGeneralSettings([FromForm] CreateOrUpdateGeneralSettingsDTO command)
        {
            var form = Request.Form;
            if (command.GeneralSettingsTrans != null && command.GeneralSettingsTrans.Any())
            {
                for (int i = 0; i < command.GeneralSettingsTrans.Count; i++)
                {
                    if (form.Files.Any(x => x.Name.Contains($"generalSettingsTrans[{i}][logoFile]")))
                        command.GeneralSettingsTrans.ElementAt(i).LogoFile = form.Files.FirstOrDefault(x => x.Name.Contains($"generalSettingsTrans[{i}][logoFile]"));
                    if (form.Files.Any(x => x.Name.Contains($"generalSettingsTrans[{i}][icoFile]")))
                        command.GeneralSettingsTrans.ElementAt(i).IcoFile = form.Files.FirstOrDefault(x => x.Name.Contains($"generalSettingsTrans[{i}][icoFile]"));
                    if (form.Files.Any(x => x.Name.Contains($"generalSettingsTrans[{i}][homeOgFile]")))
                        command.GeneralSettingsTrans.ElementAt(i).HomeOgImageFile = form.Files.FirstOrDefault(x => x.Name.Contains($"generalSettingsTrans[{i}][homeOgFile]"));
                    if (form.Files.Any(x => x.Name.Contains($"generalSettingsTrans[{i}][contactOgFile]")))
                        command.GeneralSettingsTrans.ElementAt(i).ContactOgImageFile = form.Files.FirstOrDefault(x => x.Name.Contains($"generalSettingsTrans[{i}][contactOgFile]"));
                }
            }
            return await _generalSettingsService.CreateOrUpdate(command);
        }

    }
}
