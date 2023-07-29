using MAV.Cms.Common.BaseModels;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class CustomVarResponse : BaseResponse
    {
        public string GroupName { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public virtual ICollection<CustomVarTransResponse> CustomVarTrans { get; set; }
    }
}
