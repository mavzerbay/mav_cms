using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class TranslateConfiguration : IEntityTypeConfiguration<Translate>
    {
        public void Configure(EntityTypeBuilder<Translate> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasOne(o => o.Language)
                .WithMany()
                .HasForeignKey(f => f.LanguageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
