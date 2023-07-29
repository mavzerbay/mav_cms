using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllGeneralSettingsSpecDTO : BaseSpecParams
    {
        public Guid? LanguageId { get; set; }
    }
}
