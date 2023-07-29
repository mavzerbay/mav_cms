using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Domain.Entities;
using System.Linq;

namespace MAV.Cms.Business.Mapper
{
    public class MenuMappingProfile : Profile
    {
        public MenuMappingProfile()
        {
            CreateMap<Menu, CreateMenuDTO>().ReverseMap();
            CreateMap<Menu, UpdateMenuDTO>().ReverseMap();
            CreateMap<Menu, BaseDeleteDTO>().ReverseMap();

            CreateMap<Menu, MenuResponse>().ReverseMap();
            CreateMap<Menu, BaseDropdownResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.MenuTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Name)).ReverseMap();
            CreateMap<Menu, ClientMenuResponse>()
                .ForMember(d => d.ChildMenuResponseList, o => o.Ignore())
                .ForMember(d => d.Name, o => o.MapFrom(s => s.MenuTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Name))
                .ForMember(d => d.Slug, o => o.MapFrom(s => s.MenuTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Slug))
                .ForMember(d => d.Info, o => o.MapFrom(s => s.MenuTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Info)).ReverseMap();
        }
    }
}
