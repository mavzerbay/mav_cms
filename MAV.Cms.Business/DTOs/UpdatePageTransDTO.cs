using Microsoft.AspNetCore.Http;
using System;

namespace MAV.Cms.Business.DTOs
{
    public class UpdatePageTransDTO
    {
        public Guid? Id { get; set; }
        public Guid PageId { get; set; }
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Summary { get; set; }
        public string Content { get; set; }
        /// <summary>
        /// Sayfa üstü resmi için
        /// </summary>
        public string HeaderPath { get; set; }
        public IFormFile HeaderFile { get; set; }
        /// <summary>
        /// Listeleme ekranlarında görünmesi için
        /// </summary>
        public string BackgroundPath { get; set; }
        public IFormFile BackgroundFile { get; set; }
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
        public IFormFile OgImageFile { get; set; }
        public string OgImagePath { get; set; }
        /// <summary>
        /// article,video etc. https://ogp.me/#types
        /// </summary>
        public string OgType { get; set; }
        #endregion
    }
}