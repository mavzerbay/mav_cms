
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Interfaces;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MAV.Lister.API.Controller
{
    public class SupportTicketController : BaseApiController
    {
        private readonly ISupportTicketService _supportTicketService;

        public SupportTicketController(ISupportTicketService supportTicketService)
        {
            _supportTicketService = supportTicketService ?? throw new ArgumentNullException(nameof(supportTicketService));
        }

        [HttpGet("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<SupportTicketResponse>> GetSupportTicketById(Guid Id) => await _supportTicketService.GetById(Id);

        [HttpGet]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<SupportTicketResponse>> GetAllSupportTicket([FromQuery] GetAllSupportTicketSpecDTO query) => await _supportTicketService.GetAll(query);

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ApiResponse<SupportTicketResponse>> CreateSupportTicket([FromBody] CreateSupportTicketDTO command) => await _supportTicketService.Create(command);

        [HttpPut]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<SupportTicketResponse>> UpdateSupportTicket([FromBody] UpdateSupportTicketDTO command) => await _supportTicketService.Update(command);

        [HttpDelete("{Id:Guid}")]
        [Authorize(Roles = "Admin,SuperAdmin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ApiResponse<bool>> DeleteSupportTicket(Guid Id) => await _supportTicketService.Delete(new BaseDeleteDTO(Id));

    }
}
