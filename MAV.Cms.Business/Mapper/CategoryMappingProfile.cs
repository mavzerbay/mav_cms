using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Domain.Entities;
using System.Linq;

namespace MAV.Cms.Business.Mapper
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<Category, CreateCategoryDTO>().ReverseMap();
            CreateMap<Category, UpdateCategoryDTO>().ReverseMap();
            CreateMap<Category, BaseDeleteDTO>().ReverseMap();

            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Category, BaseDropdownResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.CategoryTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Name)).ReverseMap();
        }
    }
}
