using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.DTOs.SpecDTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.Helpers;
using System;
using System.Threading.Tasks;

namespace MAV.Cms.Business.Interfaces
{
    public interface ISupportTicketService
    {
        Task<ApiResponse<SupportTicketResponse>> Create(CreateSupportTicketDTO request);
        Task<ApiResponse<SupportTicketResponse>> Update(UpdateSupportTicketDTO request);
        Task<ApiResponse<bool>> Delete(BaseDeleteDTO request);
        Task<ApiResponse<SupportTicketResponse>> GetById(Guid Id);
        Task<ApiResponse<SupportTicketResponse>> GetAll(GetAllSupportTicketSpecDTO request);
    }
}
