using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class ClientMenuResponse : BaseResponse
    {
        public int DisplayOrder { get; set; }
        public string RouterLink { get; set; }
        public string RouterQueryParameter { get; set; }
        public string Icon { get; set; }
        public string BackgroundImagePath { get; set; }
        /// <summary>
        /// GroupName MenuType olanlar listelenir. (List,Grid,Detail)
        /// </summary>
        public Guid? MenuTypeId { get; set; }
        public virtual CustomVarDropdownResponse MenuType { get; set; }
        public Guid? ParentMenuId { get; set; }
        public virtual BaseDropdownResponse ParentMenu { get; set; }
        /// <summary>
        /// IsBackend false olanlar için sayfa id eğer varsa tıklandığı zaman o sayfanın bilgisi döner
        /// </summary>
        public Guid? PageId { get; set; }
        public virtual PageDropdownResponse Page { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
        public string Slug { get; set; }
        public ICollection<ClientMenuResponse> ChildMenuResponseList { get; set; }
    }
}
