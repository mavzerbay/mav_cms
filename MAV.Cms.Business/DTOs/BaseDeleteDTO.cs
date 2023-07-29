using System;

namespace MAV.Cms.Business.DTOs
{
    public class BaseDeleteDTO
    {
        public BaseDeleteDTO(Guid id, bool isSoftDelete = true)
        {
            Id = id;
            IsSoftDelete = isSoftDelete;
        }
        public Guid Id { get; set; }
        public bool IsSoftDelete { get; set; } = true;
    }
}
