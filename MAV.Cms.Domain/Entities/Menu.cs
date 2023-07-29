using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class Menu : BaseEntity
    {
        public int DisplayOrder { get; set; }
        public bool Activity { get; set; }
        public string RouterLink { get; set; }
        public string RouterQueryParameter { get; set; }
        public string Icon { get; set; }
        public string BackgroundImagePath { get; set; }
        public bool IsBackend { get; set; } = false;
        /// <summary>
        /// GroupName MenuPosition olanlar listelenir. (Header,Bottom)
        /// </summary>
        public Guid? MenuPositionId { get; set; }
        [ForeignKey("MenuPositionId")]
        public CustomVar MenuPosition { get; set; }
        public Guid? ParentMenuId { get; set; }
        [ForeignKey("ParentMenuId")]
        public Menu ParentMenu { get; set; }
        /// <summary>
        /// IsBackend false olanlar için sayfa id eğer varsa tıklandığı zaman o sayfanın bilgisi döner
        /// </summary>
        public Guid? PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; }
        public virtual ICollection<MenuTrans> MenuTrans { get; set; }

    }
}
