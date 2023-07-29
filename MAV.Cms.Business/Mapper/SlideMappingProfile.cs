using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class SlideMappingProfile : Profile
    {
        public SlideMappingProfile()
        {
            CreateMap<Slide, CreateSlideDTO>().ReverseMap();
            CreateMap<Slide, UpdateSlideDTO>().ReverseMap();
            CreateMap<Slide, BaseDeleteDTO>().ReverseMap();

            CreateMap<Slide, SlideResponse>().ReverseMap();
            CreateMap<Slide, BaseDropdownResponse>().ReverseMap();
        }
    }
}
