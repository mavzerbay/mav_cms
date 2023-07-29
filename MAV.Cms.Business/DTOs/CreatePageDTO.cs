using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class CreatePageDTO
    {
        public bool Activity { get; set; }
        public Guid? ParentPageId { get; set; }
        /// <summary>
        /// Doluysa aynı kategorideki sayfaları sayfa yapısına göre listelemek için
        /// </summary>
        public Guid? CategoryId { get; set; }
        /// <summary>
        /// GroupName PageType olanlar listelenir. (List,Grid,Detail)
        /// </summary>
        public Guid? PageTypeId { get; set; }
        public virtual ICollection<CreatePageTransDTO> PageTrans { get; set; }
    }
}
