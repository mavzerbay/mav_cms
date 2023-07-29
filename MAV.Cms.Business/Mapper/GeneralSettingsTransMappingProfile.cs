using AutoMapper;
using MAV.Cms.Business.DTOs;
using MAV.Cms.Business.Responses;
using MAV.Cms.Domain.Entities;

namespace MAV.Cms.Business.Mapper
{
    public class GeneralSettingsTransMappingProfile : Profile
    {
        public GeneralSettingsTransMappingProfile()
        {
            CreateMap<GeneralSettingsTrans, CreateOrUpdateGeneralSettingsTransDTO>().ReverseMap();
            CreateMap<GeneralSettingsTrans, BaseDeleteDTO>().ReverseMap();

            CreateMap<GeneralSettingsTrans, GeneralSettingsTransResponse>().ReverseMap();
        }
    }
}
