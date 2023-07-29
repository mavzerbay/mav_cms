using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class UpdateCustomVarDTO
    {
        public Guid Id { get; set; }
        public string GroupName { get; set; }
        public string KeyName { get; set; }
        public string Value { get; set; }
        public ICollection<UpdateCustomVarTransDTO> CustomVarTrans { get; set; }
    }
}
