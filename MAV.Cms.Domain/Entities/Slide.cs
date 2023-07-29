using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class Slide : BaseEntity
    {
        public bool Activity { get; set; }
        public bool isHome { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Sayfa galerisi için kullanırsa sayfa id si verilir
        /// </summary>
        public Guid? PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; }
        /// <summary>
        /// GroupName SlidePosition olanlar listelenir.
        /// </summary>
        public Guid SlidePositionId { get; set; }
        [ForeignKey("SlidePositionId")]
        public CustomVar SlidePosition { get; set; }
        public virtual ICollection<SlideMedia> SlideMedias { get; set; }
    }
}
