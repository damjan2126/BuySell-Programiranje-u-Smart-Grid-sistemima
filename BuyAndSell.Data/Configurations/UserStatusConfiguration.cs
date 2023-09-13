using BuySell.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuySell.Data.Configurations
{
    internal class UserStatusConfiguration : IEntityTypeConfiguration<UserStatus>
    {
        public void Configure(EntityTypeBuilder<UserStatus> builder)
        {
            builder.HasIndex(x => x.Status);

            builder.HasOne(x => x.User)
                .WithMany(x => x.Statuses)
                .HasForeignKey(x => x.UserId);
        }
    }
}
