using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace MAV.Cms.Domain.Entities.Identity
{
    public class MavRole : IdentityRole<Guid>
    {
        public virtual ICollection<MavUserRole> UserRoles { get; set; }
    }
}