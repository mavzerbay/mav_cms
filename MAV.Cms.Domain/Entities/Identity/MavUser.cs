using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MAV.Cms.Domain.Entities.Identity
{
    public class MavUser : IdentityUser<Guid>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string NameSurname
        {
            get
            {
                return $"{Name} {Surname}";
            }
        }
        public string WorkPhoneNumber { get; set; }
        public string WorkEmail { get; set; }
        public string ProfileImagePath { get; set; }
        public virtual ICollection<MavUserRole> UserRoles { get; set; }
        #region BaseEntity
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public Guid? CreatedById { get; set; }
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
        #endregion
    }
}
