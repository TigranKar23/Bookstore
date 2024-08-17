using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.DAL.Models;

namespace Bookstore.DAL.Configurations
{
    public class BookUserConfiguration : BaseConfiguration<BookUser>
    {
        public override void Configure(EntityTypeBuilder<BookUser> builder)
        {
            base.Configure(builder);

            builder.HasKey(bu => new { bu.BookId, bu.UserId });

            builder.HasOne(bu => bu.Book)
                .WithMany(b => b.BookUsers)
                .HasForeignKey(bu => bu.BookId);

            builder.HasOne(bu => bu.User)
                .WithMany(u => u.BookUsers)
                .HasForeignKey(bu => bu.UserId);
        }
    }
}