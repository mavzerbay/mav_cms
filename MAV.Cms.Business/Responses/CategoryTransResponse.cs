using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.Responses
{
    public class CategoryTransResponse : BaseResponse
    {
        public Guid CategoryId { get; set; }
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string Info { get; set; }
    }
}
