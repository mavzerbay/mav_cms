using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities.Identity;

namespace MAV.Cms.Business.Mapper
{
    public class AccountMappingProfile : Profile
    {
        public AccountMappingProfile()
        {
            CreateMap<MavUser, UserResponse>().ReverseMap();
            CreateMap<MavUser, CreateAccountDTO>().ReverseMap();
        }
    }
}
