
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
    public class SlideController : BaseApiController
    {
        private readonly ISlideService _slideService;

        public SlideController(ISlideService slideService)
        {
            _slideService = slideService ?? throw new ArgumentNullException(nameof(slideService));
        }

        [HttpGet("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<SlideResponse>> GetSlideById(Guid Id) => await _slideService.GetById(Id);

        [HttpGet("GetByPageId/{Id:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<SlideResponse>> GetSlideByPageId(Guid Id) => await _slideService.GetByPageId(Id);

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<SlideResponse>> GetAllSlide([FromQuery] GetAllSlideSpecDTO query) => await _slideService.GetAll(query);


        [HttpGet("GetHomeSlide")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<SlideResponse>> GetHomeSlide() => await _slideService.GetHomeSlide();


        [HttpGet("GetTestimonialSlide")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<SlideResponse>> GetTestimonialSlide() => await _slideService.GetTestimonialSlide();

        [HttpGet("GetDropdownList")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<BaseDropdownResponse>> GetDropdownList([FromQuery] GetAllSlideSpecDTO query) => await _slideService.GetDropdownList(query);


        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<SlideResponse>> CreateSlide([FromForm] CreateSlideDTO command)
        {

            var form = Request.Form;
            if (command.SlideMedias != null && command.SlideMedias.Any())
            {
                for (int i = 0; i < command.SlideMedias.Count; i++)
                {
                    if (form.Files.Any(x => x.Name.Contains($"slideMedias[{i}][backgroundImageFile]")))
                        command.SlideMedias.ElementAt(i).BackgroundImageFile = form.Files.FirstOrDefault(x => x.Name.Contains($"slideMedias[{i}][backgroundImageFile]"));
                }
            }
            return await _slideService.Create(command);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<SlideResponse>> UpdateSlide([FromForm] UpdateSlideDTO command)
        {
            var form = Request.Form;
            if (command.SlideMedias != null && command.SlideMedias.Any())
            {
                for (int i = 0; i < command.SlideMedias.Count; i++)
                {
                    if (form.Files.Any(x => x.Name.Contains($"slideMedias[{i}][backgroundImageFile]")))
                        command.SlideMedias.ElementAt(i).BackgroundImageFile = form.Files.FirstOrDefault(x => x.Name.Contains($"slideMedias[{i}][backgroundImageFile]"));
                }
            }

            return await _slideService.Update(command);
        }

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<bool>> DeleteSlide(Guid Id) => await _slideService.Delete(new BaseDeleteDTO(Id));

    }
}
