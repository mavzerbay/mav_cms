using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Domain.Entities;
using System.Linq;

namespace MAV.Cms.Business.Mapper
{
    public class CustomVarMappingProfile : Profile
    {
        public CustomVarMappingProfile()
        {
            CreateMap<CustomVar, CreateCustomVarDTO>().ReverseMap();
            CreateMap<CustomVar, UpdateCustomVarDTO>().ReverseMap();
            CreateMap<CustomVar, BaseDeleteDTO>().ReverseMap();

            CreateMap<CustomVar, CustomVarResponse>().ReverseMap();
            CreateMap<CustomVar, BaseDropdownResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.CustomVarTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Name)).ReverseMap();
            CreateMap<CustomVar, CustomVarDropdownResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.CustomVarTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Name)).ReverseMap();
        }
    }
}
