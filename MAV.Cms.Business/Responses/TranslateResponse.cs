using MAV.Cms.Common.BaseModels;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Business.Responses
{
    public class TranslateResponse : BaseResponse
    {
        public Guid LanguageId { get; set; }
        [NotMapped]
        public virtual BaseDropdownResponse Language { get; set; }
        public string KeyName { get; set; }
        public string Translation { get; set; }
        public string Explanation { get; set; }
    }
}
