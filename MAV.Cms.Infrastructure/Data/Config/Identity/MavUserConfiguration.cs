using MAV.Cms.Domain.Entities;
using MAV.Cms.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class MavUserConfiguration : IEntityTypeConfiguration<MavUser>
    {
        public void Configure(EntityTypeBuilder<MavUser> builder)
        {
            builder.ToTable("mav_user");

            builder.HasMany(ur => ur.UserRoles)
                .WithOne(u => u.User)
                .HasForeignKey(ur => ur.UserId)
                .IsRequired();
        }
    }
}
