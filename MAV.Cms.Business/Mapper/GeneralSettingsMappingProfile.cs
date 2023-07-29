using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Common.BaseModels;
using MAV.Cms.Common.Helpers;
using MAV.Cms.Domain.Entities;
using System.Linq;

namespace MAV.Cms.Business.Mapper
{
    public class GeneralSettingsMappingProfile : Profile
    {
        public GeneralSettingsMappingProfile()
        {
            CreateMap<GeneralSettings, CreateOrUpdateGeneralSettingsDTO>().ReverseMap();
            CreateMap<GeneralSettings, BaseDeleteDTO>().ReverseMap();

            CreateMap<GeneralSettings, GeneralSettingsResponse>().ReverseMap();
        }
    }
}
