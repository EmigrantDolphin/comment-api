
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class UserEntityTypeConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.Username)
                .IsUnique();

            builder.Property(x => x.Username)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.Password)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.FullName)
                .HasMaxLength(256)
                .IsRequired();

            builder.HasMany(x => x.Comments)
                .WithOne(x => x.User)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}