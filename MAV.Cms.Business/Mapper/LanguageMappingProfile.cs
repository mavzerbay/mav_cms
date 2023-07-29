using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class LanguageMappingProfile : Profile
    {
        public LanguageMappingProfile()
        {
            CreateMap<Language, CreateLanguageDTO>().ReverseMap();
            CreateMap<Language, UpdateLanguageDTO>().ReverseMap();
            CreateMap<Language, BaseDeleteDTO>().ReverseMap();

            CreateMap<Language, LanguageResponse>().ReverseMap();
            CreateMap<Language, BaseDropdownResponse>().ReverseMap();
        }
    }
}
