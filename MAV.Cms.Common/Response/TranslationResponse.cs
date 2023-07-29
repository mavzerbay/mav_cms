using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Common.Response
{
    public class TranslationResponse : BaseResponse
    {
        public string KeyName { get; set; }
        public string Translation { get; set; }
        public Guid LanguageId { get; set; }
    }
}
