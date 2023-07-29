using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class MenuTransMappingProfile : Profile
    {
        public MenuTransMappingProfile()
        {
            CreateMap<MenuTrans, CreateMenuTransDTO>().ReverseMap();
            CreateMap<MenuTrans, UpdateMenuTransDTO>().ReverseMap();
            CreateMap<MenuTrans, BaseDeleteDTO>().ReverseMap();

            CreateMap<MenuTrans, MenuTransResponse>().ReverseMap();
        }
    }
}
