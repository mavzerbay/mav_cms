using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class GeneralSettingsTrans : BaseEntity
    {
        public Guid GeneralSettingsId { get; set; }
        [ForeignKey("GeneralSettingsId")]
        public GeneralSettings GeneralSettings { get; set; }
        public Guid LanguageId { get; set; }
        [ForeignKey("LanguageId")]
        public Language Language { get; set; }
        public string LogoPath { get; set; }
        public string IcoPath { get; set; }
        public string AboutUs { get; set; }
        #region Home Info Sections 
        public string Info1Icon { get; set; }
        public string Info1Title { get; set; }
        public string Info1Description { get; set; }
        public string Info2Icon { get; set; }
        public string Info2Title { get; set; }
        public string Info2Description { get; set; }
        public string Info3Icon { get; set; }
        public string Info3Title { get; set; }
        public string Info3Description { get; set; }
        public string Info4Icon { get; set; }
        public string Info4Title { get; set; }
        public string Info4Description { get; set; }
        #endregion
        /// <summary>
        /// SEO Alanları Anasayfa-İletişim gibi sabit sayfalar için
        /// </summary>
        #region SEO areas for Home-Contact 
        public string HomeOgTitle { get; set; }
        public string HomeOgDescription { get; set; }
        public string HomeOgKeywords { get; set; }
        /// <summary>
        /// Use images with a 1.91:1 ratio,1200x630 for optimal clarity across all devices.
        /// </summary>
        public string HomeOgImage { get; set; }
        public string ContactOgTitle { get; set; }
        public string ContactOgDescription { get; set; }
        public string ContactOgKeywords { get; set; }
        /// <summary>
        /// Use images with a 1.91:1 ratio,1200x630 for optimal clarity across all devices.
        /// </summary>
        public string ContactOgImage { get; set; }

        #endregion
    }
}