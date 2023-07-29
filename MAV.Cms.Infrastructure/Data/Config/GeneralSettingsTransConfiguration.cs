using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class GeneralSettingsTransConfiguration : IEntityTypeConfiguration<GeneralSettingsTrans>
    {
        public void Configure(EntityTypeBuilder<GeneralSettingsTrans> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasOne(o => o.GeneralSettings)
                .WithMany(m => m.GeneralSettingsTrans)
                .HasForeignKey(f => f.GeneralSettingsId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Language)
                .WithMany()
                .HasForeignKey(f => f.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
