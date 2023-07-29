using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllTranslateSpecDTO : BaseSpecParams
    {
        public Guid? LanguageId { get; set; }
        public string KeyName { get; set; }
        public string Translation { get; set; }
        public string Explanation { get; set; }
    }
}
