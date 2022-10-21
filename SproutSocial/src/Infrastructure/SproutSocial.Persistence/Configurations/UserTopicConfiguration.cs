using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class UserTopicConfiguration : IEntityTypeConfiguration<UserTopic>
{
    public void Configure(EntityTypeBuilder<UserTopic> builder)
    {
        builder.Property(ut => ut.AppUserId)
            .IsRequired(true);
        builder.Property(ut => ut.TopicId)
            .IsRequired(true);
        builder
            .HasOne(ut => ut.Topic)
            .WithMany(t => t.UserTopics)
            .HasForeignKey(ut => ut.TopicId);
        builder
            .HasOne(ut => ut.AppUser)
            .WithMany(u => u.UserTopics)
            .HasForeignKey(ut => ut.AppUserId);
    }
}
