using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Common.BaseModels
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid CreatedById { get; set; }
        public string CreatedLocalIp { get; set; }
        public string CreatedRemoteIp { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public Guid? UpdatedById { get; set; }
        public string UpdatedLocalIp { get; set; }
        public string UpdatedRemoteIp { get; set; }
        public DateTime? DeletedDate { get; set; }
        public Guid? DeletedById { get; set; }
        public string DeletedLocalIp { get; set; }
        public string DeletedRemoteIp { get; set; }
        [NotMapped]
        public bool? isSoftDelete { get; set; }
    }
}
