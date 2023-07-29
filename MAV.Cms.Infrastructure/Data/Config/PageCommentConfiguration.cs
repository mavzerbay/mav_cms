using MAV.Cms.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MAV.Cms.Infrastructure.Data.Config
{
    public class PageCommentConfiguration : IEntityTypeConfiguration<PageComment>
    {
        public void Configure(EntityTypeBuilder<PageComment> builder)
        {
            builder.HasQueryFilter(f => f.DeletedDate == null);

            builder.HasOne(o => o.Page)
                .WithMany(m => m.PageComments)
                .HasForeignKey(f => f.PageId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
