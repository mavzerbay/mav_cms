using MAV.Cms.Common.BaseModels;
using MAV.Cms.Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class Page : BaseEntity
    {
        public virtual MavUser CreatedBy { get; set; }
        public bool Activity { get; set; }
        public Guid? ParentPageId { get; set; }
        [ForeignKey("ParentPageId")]
        public Page ParentPage { get; set; }
        /// <summary>
        /// Doluysa aynı kategorideki sayfaları sayfa yapısına göre listelemek için
        /// </summary>
        public Guid? CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        /// <summary>
        /// GroupName PageType olanlar listelenir. (List,Grid,Detail)
        /// </summary>
        public Guid? PageTypeId { get; set; }
        [ForeignKey("PageTypeId")]
        public CustomVar PageType { get; set; }
        public virtual ICollection<PageTrans> PageTrans { get; set; }
        public virtual ICollection<PageComment> PageComments { get; set; }
        public virtual ICollection<Page> ChildPageList { get; set; }
    }
}