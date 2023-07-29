using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Response;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class TranslateMappingProfile : Profile
    {
        public TranslateMappingProfile()
        {
            CreateMap<Translate, CreateTranslateDTO>().ReverseMap();
            CreateMap<Translate, UpdateTranslateDTO>().ReverseMap();
            CreateMap<Translate, BaseDeleteDTO>().ReverseMap();

            CreateMap<Translate, TranslateResponse>().ReverseMap();
            CreateMap<Translate, TranslationResponse>().ReverseMap();
        }
    }
}
