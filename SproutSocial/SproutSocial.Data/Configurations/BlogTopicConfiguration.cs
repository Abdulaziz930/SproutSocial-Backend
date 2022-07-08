using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SproutSocial.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SproutSocial.Data.Configurations
{
    public class BlogTopicConfiguration : IEntityTypeConfiguration<BlogTopic>
    {
        public void Configure(EntityTypeBuilder<BlogTopic> builder)
        {
            builder.HasOne(x => x.Blog).WithMany(x => x.BlogTopics).HasForeignKey(x => x.BlogId);
            builder.HasOne(x => x.Topic).WithMany(x => x.BlogTopics).HasForeignKey(x => x.TopicId);
        }
    }
}
