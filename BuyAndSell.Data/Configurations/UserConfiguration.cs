using BuySell.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace BuySell.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasMany(x => x.Roles)
            .WithMany(y => y.Users)
            .UsingEntity<IdentityUserRole<long>>
            (
                role => role.HasOne<Role>()
                    .WithMany()
                    .HasForeignKey(r => r.RoleId)
                    .IsRequired(),
                role => role.HasOne<User>()
                    .WithMany()
                    .HasForeignKey(r => r.UserId)
                    .IsRequired()
            );

            builder.HasMany<UserStatus>()
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId);

            builder.HasMany<UserStatus>()
                .WithOne(x => x.UpdatedByUser)
                .HasForeignKey(x => x.UpdatedByUserId);

            builder.HasMany<UserStatus>()
                .WithOne(x => x.CreatedByUser)
                .HasForeignKey(x => x.CreatedByUserId);
        }
    }
}
