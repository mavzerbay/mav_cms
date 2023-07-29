using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class UpdateMenuDTO
    {
        public Guid Id { get; set; }
        public int DisplayOrder { get; set; }
        public bool Activity { get; set; }
        public string RouterLink { get; set; }
        public string RouterQueryParameter { get; set; }
        public string Icon { get; set; }
        public string BackgroundImagePath { get; set; }
        public IFormFile BackgroundImageFile { get; set; }
        public bool IsBackend { get; set; } = false;
        /// <summary>
        /// GroupName MenuPosition olanlar listelenir. (Header,Bottom)
        /// </summary>
        public Guid? MenuPositionId { get; set; }
        public Guid? ParentMenuId { get; set; }
        /// <summary>
        /// IsBackend false olanlar için sayfa id eğer varsa tıklandığı zaman o sayfanın bilgisi döner
        /// </summary>
        public Guid? PageId { get; set; }
        public virtual ICollection<UpdateMenuTransDTO> MenuTrans { get; set; }
    }
}
