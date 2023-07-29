using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class ClientPageResponse : BaseResponse
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
        public Guid CreatedById { get; set; }
        public virtual BaseDropdownResponse CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        #region Trans Area
        public string Name { get; set; }
        public string Slug { get; set; }
        public virtual ICollection<PageTransDropdownResponse> LanguageSlugList { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// Sayfa üstü resmi için
        /// </summary>
        public string HeaderPath { get; set; }
        /// <summary>
        /// Listeleme ekranlarında görünmesi için
        /// </summary>
        public string BackgroundPath { get; set; }
        #region SEO
        /// <summary>
        /// SEO Alanları
        /// </summary>
        public string OgTitle { get; set; }
        public string OgDescription { get; set; }
        public string OgKeywords { get; set; }
        /// <summary>
        /// Use images with a 1.91:1 ratio,1200x630 for optimal clarity across all devices.
        /// </summary>
        public string OgImagePath { get; set; }
        /// <summary>
        /// article,video etc. https://ogp.me/#types
        /// </summary>
        public string OgType { get; set; }
        #endregion
        #endregion
        public virtual ICollection<ClientPageResponse> ChildPageList { get; set; }
    }
}
