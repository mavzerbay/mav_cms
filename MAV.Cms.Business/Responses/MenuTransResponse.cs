using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.Responses
{
    public class MenuTransResponse : BaseResponse
    {
        public Guid MenuId { get; set; }
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public string Info { get; set; }
    }
}
