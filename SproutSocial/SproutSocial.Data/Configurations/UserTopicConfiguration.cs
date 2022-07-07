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
    public class UserTopicConfiguration : IEntityTypeConfiguration<UserTopic>
    {
        public void Configure(EntityTypeBuilder<UserTopic> builder)
        {
            builder.HasOne(x => x.User).WithMany(x => x.UserTopics).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Topic).WithMany(x => x.UserTopics).HasForeignKey(x => x.TopicId);
        }
    }
}
