using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class CustomVarConfiguration : IEntityTypeConfiguration<CustomVar>
    {
        public void Configure(EntityTypeBuilder<CustomVar> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);
        }
    }
}
