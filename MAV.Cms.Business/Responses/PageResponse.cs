using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class PageResponse : BaseResponse
    {
        public bool Activity { get; set; }
        public Guid? ParentPageId { get; set; }
        public virtual PageDropdownResponse ParentPage { get; set; }
        /// <summary>
        /// Doluysa aynı kategorideki sayfaları sayfa yapısına göre listelemek için
        /// </summary>
        public Guid? CategoryId { get; set; }
        public virtual BaseDropdownResponse Category { get; set; }
        /// <summary>
        /// GroupName PageType olanlar listelenir. (List,Grid,Detail)
        /// </summary>
        public Guid? PageTypeId { get; set; }
        public virtual CustomVarDropdownResponse PageType { get; set; }
        public virtual ICollection<PageTransResponse> PageTrans { get; set; }
        public virtual ICollection<PageResponse> ChildPageList { get; set; }
    }
}
