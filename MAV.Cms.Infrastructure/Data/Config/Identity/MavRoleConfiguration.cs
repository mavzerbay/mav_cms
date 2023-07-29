using MAV.Cms.Domain.Entities;
using MAV.Cms.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class MavRoleConfiguration : IEntityTypeConfiguration<MavRole>
    {
        public void Configure(EntityTypeBuilder<MavRole> builder)
        {
            builder.ToTable("mav_role");

            builder.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.Role)
                .HasForeignKey(ur => ur.RoleId)
                .IsRequired();
        }
    }
}
