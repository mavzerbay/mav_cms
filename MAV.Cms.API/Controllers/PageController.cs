
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
    public class PageController : BaseApiController
    {
        private readonly IPageService _pageService;

        public PageController(IPageService pageService)
        {
            _pageService = pageService ?? throw new ArgumentNullException(nameof(pageService));
        }

        [HttpGet("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<PageResponse>> GetPageById(Guid Id) => await _pageService.GetById(Id);

        [HttpGet("{Slug}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<ClientPageResponse>> GetPageBySlug(string Slug) => await _pageService.GetBySlug(Slug);

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<PageResponse>> GetAllPage([FromQuery] GetAllPageSpecDTO query) => await _pageService.GetAll(query);


        [HttpGet("GetLatest/{ParentPageId:Guid}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<ClientPageResponse>> GetLatestProjects(Guid ParentPageId) => await _pageService.GetLatest(new GetAllPageSpecDTO { ParentPageId=ParentPageId,PageSize=3,Sort= "createdDateDesc" });



        [HttpGet("GetLatestBlogs")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<ClientPageResponse>> GetLatestBlogs() => await _pageService.GetLatestBlogs();

        [HttpGet("GetDropdownList")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<PageDropdownResponse>> GetDropdownList([FromQuery] GetAllPageSpecDTO query) => await _pageService.GetDropdownList(query);

        [HttpPost]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<PageResponse>> CreatePage([FromForm] CreatePageDTO command)
        {

            var form = Request.Form;
            if (command.PageTrans != null && command.PageTrans.Any())
            {
                for (int i = 0; i < command.PageTrans.Count; i++)
                {
                    if (form.Files.Any(x => x.Name.Contains($"pageTrans[{i}][headerFile]")))
                        command.PageTrans.ElementAt(i).HeaderFile = form.Files.FirstOrDefault(x => x.Name.Contains($"pageTrans[{i}][headerFile]"));
                    if (form.Files.Any(x => x.Name.Contains($"pageTrans[{i}][backgroundFile]")))
                        command.PageTrans.ElementAt(i).BackgroundFile = form.Files.FirstOrDefault(x => x.Name.Contains($"pageTrans[{i}][backgroundFile]"));
                    if (form.Files.Any(x => x.Name.Contains($"pageTrans[{i}][ogImageFile]")))
                        command.PageTrans.ElementAt(i).OgImageFile = form.Files.FirstOrDefault(x => x.Name.Contains($"pageTrans[{i}][ogImageFile]"));
                }
            }
            return await _pageService.Create(command);
        }

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<PageResponse>> UpdatePage([FromForm] UpdatePageDTO command)
        {

            var form = Request.Form;
            if (command.PageTrans != null && command.PageTrans.Any())
            {
                for (int i = 0; i < command.PageTrans.Count; i++)
                {
                    if (form.Files.Any(x => x.Name.Contains($"pageTrans[{i}][headerFile]")))
                        command.PageTrans.ElementAt(i).HeaderFile = form.Files.FirstOrDefault(x => x.Name.Contains($"pageTrans[{i}][headerFile]"));
                    if (form.Files.Any(x => x.Name.Contains($"pageTrans[{i}][backgroundFile]")))
                        command.PageTrans.ElementAt(i).BackgroundFile = form.Files.FirstOrDefault(x => x.Name.Contains($"pageTrans[{i}][backgroundFile]"));
                    if (form.Files.Any(x => x.Name.Contains($"pageTrans[{i}][ogImageFile]")))
                        command.PageTrans.ElementAt(i).OgImageFile = form.Files.FirstOrDefault(x => x.Name.Contains($"pageTrans[{i}][ogImageFile]"));
                }
            }
            return await _pageService.Update(command);
        }

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<bool>> DeletePage(Guid Id) => await _pageService.Delete(new BaseDeleteDTO(Id));

    }
}
