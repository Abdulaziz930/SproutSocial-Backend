using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class CommentConfiguration : IEntityTypeConfiguration<Comment>
{
    public void Configure(EntityTypeBuilder<Comment> builder)
    {
        builder.Property(c => c.Message)
            .IsRequired(true)
            .HasMaxLength(500);
        builder.Property(c => c.IsDeleted)
            .HasDefaultValue(false);

        builder
            .HasOne(c => c.Blog)
            .WithMany(b => b.Comments)
            .HasForeignKey(b => b.BlogId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(c => c.AppUser)
            .WithMany(u => u.Comments)
            .HasForeignKey(c => c.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
