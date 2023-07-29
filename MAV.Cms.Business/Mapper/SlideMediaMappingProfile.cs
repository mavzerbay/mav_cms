using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class SlideMediaMappingProfile : Profile
    {
        public SlideMediaMappingProfile()
        {
            CreateMap<SlideMedia, CreateSlideMediaDTO>().ReverseMap();
            CreateMap<SlideMedia, UpdateSlideMediaDTO>().ReverseMap();
            CreateMap<SlideMedia, BaseDeleteDTO>().ReverseMap();

            CreateMap<SlideMedia, SlideMediaResponse>().ReverseMap();
        }
    }
}
