using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class CustomVarTransConfiguration : IEntityTypeConfiguration<CustomVarTrans>
    {
        public void Configure(EntityTypeBuilder<CustomVarTrans> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasOne(o => o.CustomVar)
                .WithMany(m => m.CustomVarTrans)
                .HasForeignKey(f => f.CustomVarId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Language)
                .WithMany()
                .HasForeignKey(f => f.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
