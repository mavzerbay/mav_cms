using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class SupportTicketMappingProfile : Profile
    {
        public SupportTicketMappingProfile()
        {
            CreateMap<SupportTicket, CreateSupportTicketDTO>().ReverseMap();
            CreateMap<SupportTicket, UpdateSupportTicketDTO>().ReverseMap();
            CreateMap<SupportTicket, BaseDeleteDTO>().ReverseMap();

            CreateMap<SupportTicket, SupportTicketResponse>().ReverseMap();
        }
    }
}
