using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.DTOs.SpecDTOs
{
    public class GetAllPageSpecDTO : BaseSpecParams
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public string PageTypeKeyName { get; set; }
        public bool? Activity { get; set; }
        public bool? IncludeChild { get; set; }
        public bool? IncludeCreatedBy { get; set; }
        public Guid? LanguageId { get; set; }
        public Guid? CategoryId { get; set; }
        public Guid? PageTypeId { get; set; }
        public Guid? ParentPageId { get; set; }
        /// <summary>
        /// Kendisi hariç getirmesi için
        /// </summary>
        public Guid? PageId { get; set; }
        public bool? OnlyParent { get; set; }
    }
}
