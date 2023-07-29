using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class GeneralSettingsConfiguration : IEntityTypeConfiguration<GeneralSettings>
    {
        public void Configure(EntityTypeBuilder<GeneralSettings> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);
        }
    }
}
