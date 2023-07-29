using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities
{
    public class GeneralSettings : BaseEntity
    {
        #region SendMail Settings
        public string SmtpHost { get; set; }
        public string SmtpPort { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        #endregion
        /// <summary>
        /// Bilgilendirme mesajlarının gönderileceği email
        /// </summary>
        public string SupportMail { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string ContactWhatsApp { get; set; }
        public string GoogleMapUrl { get; set; }
        public string LinkedInURL { get; set; }
        public string FacebookURL { get; set; }
        public string InstagramURL { get; set; }
        #region Home Projects Section
        public int YearsOfExperienced { get; set; }
        public int ProjectDone { get; set; }
        public int HappyCustomer { get; set; }
        #endregion
        public Guid? TestimonialSlideId { get; set; }
        [ForeignKey("TestimonialSlideId")]
        public Slide TestimonialSlide { get; set; }
        /// <summary>
        /// Anasayfadaki en son projeler-ürünler alanı için seçilmesi gereken parentPage
        /// </summary>
        public Guid? LatestProjectPageId { get; set; }
        [ForeignKey("LatestProjectPageId")]
        public Page LatestProjectPage { get; set; }
        public virtual ICollection<GeneralSettingsTrans> GeneralSettingsTrans { get; set; }
    }
}