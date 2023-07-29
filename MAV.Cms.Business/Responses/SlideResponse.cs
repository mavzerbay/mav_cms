using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class SlideResponse : BaseResponse
    {
        public bool Activity { get; set; }
        public bool isHome { get; set; }
        public string Name { get; set; }
        /// <summary>
        /// Sayfa galerisi için kullanırsa sayfa id si verilir
        /// </summary>
        public Guid? PageId { get; set; }
        public virtual PageDropdownResponse Page { get; set; }
        /// <summary>
        /// GroupName SlidePosition olanlar listelenir.
        /// </summary>
        public Guid SlidePositionId { get; set; }
        public virtual CustomVarDropdownResponse SlidePosition { get; set; }
        public virtual ICollection<SlideMediaResponse> SlideMedias { get; set; }
    }
}
