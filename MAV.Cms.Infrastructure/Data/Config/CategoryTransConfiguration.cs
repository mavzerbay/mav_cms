using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class CategoryTransConfiguration : IEntityTypeConfiguration<CategoryTrans>
    {
        public void Configure(EntityTypeBuilder<CategoryTrans> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasOne(o => o.Category)
                .WithMany(m => m.CategoryTrans)
                .HasForeignKey(f => f.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Language)
                .WithMany()
                .HasForeignKey(f => f.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
