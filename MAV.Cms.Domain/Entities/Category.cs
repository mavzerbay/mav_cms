using MAV.Cms.Common.BaseModels;
using System.Collections.Generic;

namespace MAV.Cms.Domain.Entities
{
    public class Category : BaseEntity
    {
        public bool Activity { get; set; }
        public virtual ICollection<CategoryTrans> CategoryTrans { get; set; }
    }
}