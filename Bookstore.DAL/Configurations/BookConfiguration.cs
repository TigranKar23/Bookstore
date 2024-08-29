using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.DAL.Models;

namespace Bookstore.DAL.Configurations
{
    public class BookConfiguration : BaseConfiguration<Book>
    {
        public override void Configure(EntityTypeBuilder<Book> builder)
        {
            base.Configure(builder);

            builder.Property(b => b.Title)
                .IsRequired(false)
                .HasMaxLength(255);

            builder.Property(b => b.DateOfRelease)
                .IsRequired();
            
            builder.Property(b => b.Count)
                .IsRequired();
            
            builder.Property(b => b.IsAvailable)
                .IsRequired();

            builder.HasMany(b => b.BookAuthors)
                .WithOne(ba => ba.Book)
                .HasForeignKey(ba => ba.BookId);

            builder.HasMany(b => b.BookUsers)
                .WithOne(bu => bu.Book)
                .HasForeignKey(bu => bu.BookId);
        }
    }
}