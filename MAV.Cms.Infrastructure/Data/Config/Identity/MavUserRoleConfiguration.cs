using MAV.Cms.Domain.Entities;
using MAV.Cms.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class MavUserRoleConfiguration : IEntityTypeConfiguration<MavUserRole>
    {
        public void Configure(EntityTypeBuilder<MavUserRole> builder)
        {
            builder.ToTable("mav_user_role");
            
            builder.HasKey(x => new { x.UserId, x.RoleId });
        }
    }
}
