using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Domain.Entities;
using System.Linq;

namespace MAV.Cms.Business.Mapper
{
    public class PageMappingProfile : Profile
    {
        public PageMappingProfile()
        {
            CreateMap<Page, CreatePageDTO>().ReverseMap();
            CreateMap<Page, UpdatePageDTO>().ReverseMap();
            CreateMap<Page, BaseDeleteDTO>().ReverseMap();

            CreateMap<Page, PageResponse>().ReverseMap();

            CreateMap<Page, PageDropdownResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Name))
                .ForMember(d => d.Slug, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Slug)).ReverseMap();

            CreateMap<Page, ClientPageResponse>()
                .ForMember(d => d.Name, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Name))
                .ForMember(d => d.Slug, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Slug))
                .ForMember(d => d.LanguageSlugList, o => o.MapFrom(s => s.PageTrans))
                .ForMember(d => d.Summary, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Summary))
                .ForMember(d => d.Content, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).Content))
                .ForMember(d => d.HeaderPath, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).HeaderPath))
                .ForMember(d => d.BackgroundPath, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).BackgroundPath))
                .ForMember(d => d.OgTitle, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).OgTitle))
                .ForMember(d => d.OgDescription, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).OgDescription))
                .ForMember(d => d.OgKeywords, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).OgKeywords))
                .ForMember(d => d.OgImagePath, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).OgImagePath))
                .ForMember(d => d.OgType, o => o.MapFrom(s => s.PageTrans.FirstOrDefault(l => l.LanguageId == LangManager.CurrentLanguageId).OgType)).ReverseMap();
        }
    }
}
