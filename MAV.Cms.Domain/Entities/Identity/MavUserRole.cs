using Microsoft.AspNetCore.Identity;
using System;

namespace MAV.Cms.Domain.Entities.Identity
{
    public class MavUserRole : IdentityUserRole<Guid>
    {
        public MavUser User { get; set; }
        public MavRole Role { get; set; }
    }
}   