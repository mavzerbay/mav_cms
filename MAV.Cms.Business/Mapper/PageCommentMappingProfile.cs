using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class PageCommentMappingProfile : Profile
    {
        public PageCommentMappingProfile()
        {
            CreateMap<PageComment, CreatePageCommentDTO>().ReverseMap();
            CreateMap<PageComment, UpdatePageCommentDTO>().ReverseMap();
            CreateMap<PageComment, BaseDeleteDTO>().ReverseMap();

            CreateMap<PageComment, PageCommentResponse>().ReverseMap();
        }
    }
}
