using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class MenuResponse : BaseResponse
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
        public virtual CustomVarDropdownResponse MenuPosition { get; set; }
        public Guid? ParentMenuId { get; set; }
        public virtual BaseDropdownResponse ParentMenu { get; set; }
        /// <summary>
        /// IsBackend false olanlar için sayfa id eğer varsa tıklandığı zaman o sayfanın bilgisi döner
        /// </summary>
        public Guid? PageId { get; set; }
        public virtual PageDropdownResponse Page { get; set; }
        public virtual ICollection<MenuTransResponse> MenuTrans { get; set; }
    }
}
