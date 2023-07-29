using System;

namespace MAV.Cms.Business.DTOs
{
    public class UpdateTranslateDTO
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
        public string KeyName { get; set; }
        public string Translation { get; set; }
        public string Explanation { get; set; }
    }
}
