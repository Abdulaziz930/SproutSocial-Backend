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
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.Property(x => x.Fullname).IsRequired(true).HasMaxLength(250);
            builder.Property(x => x.ProfilePhoto).IsRequired(false).HasMaxLength(300);
            builder.Property(x => x.IsActive).HasDefaultValue(false);
        }
    }
}
