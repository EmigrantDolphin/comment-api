using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations
{
    public class CommentEntityTypeConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.Property(x => x.Value)
                .HasMaxLength(256)
                .IsRequired();

            builder.Property(x => x.CreatedOn)
                .IsRequired();


            builder.HasOne(x => x.User)
                .WithMany(x => x.Comments)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}