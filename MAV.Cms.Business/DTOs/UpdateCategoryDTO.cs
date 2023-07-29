using System;
using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class UpdateCategoryDTO
    {
        public Guid Id { get; set; }
        public bool Activity { get; set; }
        public virtual ICollection<UpdateCategoryTransDTO> CategoryTrans { get; set; }
    }
}
