using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class CreateCustomVarDTO
    {
        public string GroupName { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public virtual ICollection<CreateCustomVarTransDTO> CustomVarTrans { get; set; }
    }
}
