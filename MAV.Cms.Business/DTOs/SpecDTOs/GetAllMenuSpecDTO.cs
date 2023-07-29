using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllMenuSpecDTO : BaseSpecParams
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Info { get; set; }
        public bool? Activity { get; set; }
        public bool? IsBackend { get; set; }
        public bool? IncludePage { get; set; }
        public int? Position { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? MenuPositionId { get; set; }
        public Guid? ParentMenuId { get; set; }
        public Guid? PageId { get; set; }
    }
}
