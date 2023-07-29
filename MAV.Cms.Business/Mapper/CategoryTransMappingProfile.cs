using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class CategoryTransMappingProfile : Profile
    {
        public CategoryTransMappingProfile()
        {
            CreateMap<CategoryTrans, CreateCategoryTransDTO>().ReverseMap();
            CreateMap<CategoryTrans, UpdateCategoryTransDTO>().ReverseMap();
            CreateMap<CategoryTrans, BaseDeleteDTO>().ReverseMap();

            CreateMap<CategoryTrans, CategoryTransResponse>().ReverseMap();
        }
    }
}
