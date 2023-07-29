using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllCategorySpecDTO : BaseSpecParams
    {
        public string Name { get; set; }
        public string ToolTip { get; set; }
        public bool? Activity { get; set; }
        public Guid? LanguageId { get; set; }
    }
}
