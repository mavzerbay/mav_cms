using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Domain.Entities.Identity;

namespace MAV.Cms.Business.Mapper
{
    public class MavUserMappingProfile : Profile
    {
        public MavUserMappingProfile()
        {
            CreateMap<MavUser, CreateAppUserDTO>().ReverseMap();
            CreateMap<MavUser, UpdateAppUserDTO>().ReverseMap();
            CreateMap<MavUser, BaseDeleteDTO>().ReverseMap();

            CreateMap<MavUser, AppUserResponse>().ReverseMap();
            CreateMap<MavUser, BaseDropdownResponse>()
                .ForMember(x => x.Name, o => o.MapFrom(s => s.NameSurname)).ReverseMap();
        }
    }
}
