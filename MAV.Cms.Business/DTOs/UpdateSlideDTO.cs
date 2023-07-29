using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class UpdateSlideDTO
    {
        public Guid Id { get; set; }
        public bool Activity { get; set; }
        public bool isHome { get; set; }
        public string Name { get; set; }
        public Guid? PageId { get; set; }
        /// <summary>
        /// GroupName SlidePosition olanlar listelenir.
        /// </summary>
        public Guid SlidePositionId { get; set; }
        public virtual ICollection<UpdateSlideMediaDTO> SlideMedias { get; set; }
    }
}
