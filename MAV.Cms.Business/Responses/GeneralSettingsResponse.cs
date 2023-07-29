using MAV.Cms.Common.BaseModels;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class GeneralSettingsResponse : BaseResponse
    {
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
        public virtual BaseDropdownResponse TestimonialSlide { get; set; }
        /// <summary>
        /// Anasayfadaki en son projeler-ürünler alanı için seçilmesi gereken parentPage
        /// </summary>
        public Guid? LatestProjectPageId { get; set; }
        public virtual PageDropdownResponse LatestProjectPage { get; set; }
        public virtual ICollection<GeneralSettingsTransResponse> GeneralSettingsTrans { get; set; }
    }
}
