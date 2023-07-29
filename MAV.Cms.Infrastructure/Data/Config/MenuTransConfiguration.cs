using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class MenuTransConfiguration : IEntityTypeConfiguration<MenuTrans>
    {
        public void Configure(EntityTypeBuilder<MenuTrans> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasIndex(c => c.Slug).IsUnique();

            builder.HasOne(o => o.Menu)
                .WithMany(m => m.MenuTrans)
                .HasForeignKey(f => f.MenuId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Language)
                .WithMany()
                .HasForeignKey(f => f.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
