using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class SlideTransConfiguration : IEntityTypeConfiguration<SlideMedia>
    {
        public void Configure(EntityTypeBuilder<SlideMedia> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasOne(o => o.Slide)
                .WithMany(m => m.SlideMedias)
                .HasForeignKey(f => f.SlideId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.Language)
                .WithMany()
                .HasForeignKey(f => f.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
