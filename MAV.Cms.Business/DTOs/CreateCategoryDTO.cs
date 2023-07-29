using System.Collections.Generic;

namespace MAV.Cms.Business.DTOs
{
    public class CreateCategoryDTO
    {
        public bool Activity { get; set; }
        public virtual ICollection<CreateCategoryTransDTO> CategoryTrans { get; set; }
    }
}
