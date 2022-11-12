using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class CommentLikeConfiguration : IEntityTypeConfiguration<CommentLike>
{
    public void Configure(EntityTypeBuilder<CommentLike> builder)
    {
        builder.Property(cl => cl.CommentId)
            .IsRequired(true);
        builder.Property(cl => cl.AppUserId)
            .IsRequired(true);

        builder
            .HasOne(cl => cl.Comment)
            .WithMany(c => c.CommentLikes)
            .HasForeignKey(cl => cl.CommentId);
        builder
            .HasOne(cl => cl.AppUser)
            .WithMany(u => u.CommentLikes)
            .HasForeignKey(cl => cl.AppUserId);
    }
}
