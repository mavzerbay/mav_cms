using Microsoft.AspNetCore.Http;
using System;

namespace MAV.Cms.Business.DTOs
{
    public class CreateSlideMediaDTO
    {
        public Guid? SlideId { get; set; }
        public Guid LanguageId { get; set; }
        public IFormFile BackgroundImageFile { get; set; }
        public string BackgroundImagePath { get; set; }
        public bool Activity { get; set; }
        public bool AffectAllLanguage { get; set; }
        public int DisplayOrder { get; set; }
        /// <summary>
        /// Ön arayüz slayt içerisindeki yazı stili slide resmi ile uyumlu değilse eklenir aksi halde temaya uygun stil uygulanır
        /// </summary>
        public string TitleTextStyle { get; set; }
        /// <summary>
        /// Ön arayüz slayt içerisindeki yazı stili slide resmi ile uyumlu değilse eklenir aksi halde temaya uygun stil uygulanır
        /// </summary>
        public string SummaryTextStyle { get; set; }
        /// <summary>
        /// Ön arayüz slayt içerisinde button varsa ve stili slide resmi ile uyumlu değilse eklenir aksi halde temaya uygun stil uygulanır
        /// </summary>
        public string ButtonStyle { get; set; }
        /// <summary>
        /// Button varsa ve yazısı değiştirilmek istenirse doldurulur aksi halde Devamı... şeklinde bir yazı görülür
        /// </summary>
        public string ButtonText { get; set; }
        /// <summary>
        /// yönlendirme yapılacak sayfanın id bilgisi bu alan doluysa routerlink i ezer
        /// </summary>
        public Guid? LinkPageId { get; set; }
        /// <summary>
        /// Yönlendirilmesi istenen bir sayfa varsa eklenir
        /// </summary>
        public string RouterLink { get; set; }
        /// <summary>
        /// Yönlendirilmesi istenen sayfanın queryParametresi varsa eklenir örn:{isTrue:true}
        /// </summary>
        public string RouterQueryParameters { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
    }
}