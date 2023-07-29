using MAV.Cms.Common.BaseModels;
using System.Collections.Generic;

namespace MAV.Cms.Domain.Entities
{
    public class CustomVar : BaseEntity
    {
        public string GroupName { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public virtual ICollection<CustomVarTrans> CustomVarTrans { get; set; }
    }
}