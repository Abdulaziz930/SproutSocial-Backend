using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class SubCommentConfiguration : IEntityTypeConfiguration<SubComment>
{
    public void Configure(EntityTypeBuilder<SubComment> builder)
    {
        builder.Property(sc => sc.Message)
            .IsRequired(true)
            .HasMaxLength(500);
        builder.Property(sc => sc.IsDeleted)
            .HasDefaultValue(false);

        builder
            .HasOne(sc => sc.Comment)
            .WithMany(c => c.SubComments)
            .HasForeignKey(sc => sc.CommentId)
            .OnDelete(DeleteBehavior.Restrict);
        builder
            .HasOne(sc => sc.AppUser)
            .WithMany(u => u.SubComments)
            .HasForeignKey(sc => sc.AppUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
