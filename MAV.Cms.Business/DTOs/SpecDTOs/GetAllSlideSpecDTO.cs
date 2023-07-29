using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllSlideSpecDTO : BaseSpecParams
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public bool? Activity { get; set; }
        public bool? MediaActivity { get; set; }
        public bool? isHome { get; set; }
        public Guid? SlidePositionId { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? PageId { get; set; }
    }
}
