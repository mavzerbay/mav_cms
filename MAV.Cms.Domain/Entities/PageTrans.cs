using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class PageTrans : BaseEntity
    {
        public Guid PageId { get; set; }
        [ForeignKey("PageId")]
        public Page Page { get; set; }
        public Guid LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
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
    }
}