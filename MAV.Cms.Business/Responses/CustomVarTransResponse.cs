using MAV.Cms.Common.BaseModels;
using System;

namespace MAV.Cms.Business.Responses
{
    public class CustomVarTransResponse : BaseResponse
    {
        public Guid CustomVarId { get; set; }
        public Guid LanguageId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
