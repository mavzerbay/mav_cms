using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Domain.Entities.Identity;

namespace MAV.Cms.Business.Mapper
{
    public class MavUserRoleMappingProfile : Profile
    {
        public MavUserRoleMappingProfile()
        {
            CreateMap<MavUserRole, BaseDropdownResponse>()
                .ForMember(x=>x.Id,o=>o.MapFrom(s=>s.RoleId))
                .ForMember(x=>x.Name,o=>o.MapFrom(s=>s.Role.Name)).ReverseMap();
        }
    }
}
