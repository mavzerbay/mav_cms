using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllCustomVarSpecDTO : BaseSpecParams
    {
        public string GroupName { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Guid? LanguageId { get; set; }
    }
}
