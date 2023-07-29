using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class CustomVarTransMappingProfile : Profile
    {
        public CustomVarTransMappingProfile()
        {
            CreateMap<CustomVarTrans, CreateCustomVarTransDTO>().ReverseMap();
            CreateMap<CustomVarTrans, UpdateCustomVarTransDTO>().ReverseMap();
            CreateMap<CustomVarTrans, BaseDeleteDTO>().ReverseMap();

            CreateMap<CustomVarTrans, CustomVarTransResponse>().ReverseMap();
        }
    }
}
