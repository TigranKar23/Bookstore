using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.DAL.Models;

namespace Bookstore.DAL.Configurations
{
    public class BookAuthorConfiguration : BaseConfiguration<BookAuthor>
    {
        public override void Configure(EntityTypeBuilder<BookAuthor> builder)
        {
            base.Configure(builder);

            builder.HasKey(ba => new { ba.BookId, ba.AuthorId });

            builder.HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            builder.HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            builder.Property(ba => ba.Role)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(ba => ba.DateAdded)
                .IsRequired();
        }
    }
}