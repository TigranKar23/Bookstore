using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.DAL.Models;

namespace Bookstore.DAL.Configurations
{
    public class ErrorConfiguration : BaseConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            // builder.ToTable("user");

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(255);

            builder.HasMany(u => u.BookUsers)
                .WithOne(bu => bu.User)
                .HasForeignKey(bu => bu.UserId);
        }
    }
}