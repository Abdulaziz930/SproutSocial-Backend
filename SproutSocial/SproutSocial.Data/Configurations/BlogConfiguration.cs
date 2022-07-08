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
    public class BlogConfiguration : IEntityTypeConfiguration<Blog>
    {
        public void Configure(EntityTypeBuilder<Blog> builder)
        {
            builder.Property(x => x.Title).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.Content).IsRequired(true).HasMaxLength(500);
            builder.Property(x => x.Image).HasMaxLength(300);
            builder.Property(x => x.IsDeleted).HasDefaultValue(false);
            builder.HasOne(x => x.User).WithMany(x => x.Blogs).HasForeignKey(x => x.UserId);
        }
    }
}
