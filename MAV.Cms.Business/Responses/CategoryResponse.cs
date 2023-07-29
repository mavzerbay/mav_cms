using MAV.Cms.Common.BaseModels;
using System.Collections.Generic;

namespace MAV.Cms.Business.Responses
{
    public class CategoryResponse : BaseResponse
    {
        public bool Activity { get; set; }
        public virtual ICollection<CategoryTransResponse> CategoryTrans { get; set; }
    }
}
