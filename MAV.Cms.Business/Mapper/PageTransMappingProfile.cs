using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class PageTransMappingProfile : Profile
    {
        public PageTransMappingProfile()
        {
            CreateMap<PageTrans, CreatePageTransDTO>().ReverseMap();
            CreateMap<PageTrans, UpdatePageTransDTO>().ReverseMap();
            CreateMap<PageTrans, BaseDeleteDTO>().ReverseMap();

            CreateMap<PageTrans, PageTransResponse>().ReverseMap();
            CreateMap<PageTrans, PageTransDropdownResponse>().ReverseMap();
        }
    }
}
