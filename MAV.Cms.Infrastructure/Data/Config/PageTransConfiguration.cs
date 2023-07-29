using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class PageTransConfiguration : IEntityTypeConfiguration<PageTrans>
    {
        public void Configure(EntityTypeBuilder<PageTrans> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasIndex(c => c.Slug).IsUnique();

            builder.HasOne(o => o.Page)
                .WithMany(m => m.PageTrans)
                .HasForeignKey(f => f.PageId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Language)
                .WithMany()
                .HasForeignKey(f => f.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
