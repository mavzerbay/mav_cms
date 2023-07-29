using System;

namespace MAV.Cms.Business.DTOs
{
    public class CreateTranslateDTO
    {
        public Guid LanguageId { get; set; }
        public string KeyName { get; set; }
        public string Translation { get; set; }
        public string Explanation { get; set; }
    }
}
