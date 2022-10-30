using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SproutSocial.Persistence.Configurations;

public class BlogTopicConfiguration : IEntityTypeConfiguration<BlogTopic>
{
    public void Configure(EntityTypeBuilder<BlogTopic> builder)
    {
        builder.ToTable("BlogTopics");
        builder.Property(bt => bt.BlogId).IsRequired(true);
        builder.Property(bt => bt.TopicId).IsRequired(true);
        builder.HasOne(bt => bt.Blog).WithMany(b => b.BlogTopics);
        builder.HasOne(bt => bt.Topic).WithMany(b => b.BlogTopics);
    }
}
