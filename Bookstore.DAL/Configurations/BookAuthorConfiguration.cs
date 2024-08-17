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

            // Составной ключ из BookId и AuthorId
            builder.HasKey(ba => new { ba.BookId, ba.AuthorId });

            // Связь с таблицей Book (многие ко многим)
            builder.HasOne(ba => ba.Book)
                .WithMany(b => b.BookAuthors)
                .HasForeignKey(ba => ba.BookId);

            // Связь с таблицей Author (многие ко многим)
            builder.HasOne(ba => ba.Author)
                .WithMany(a => a.BookAuthors)
                .HasForeignKey(ba => ba.AuthorId);

            // Настройка дополнительных свойств
            builder.Property(ba => ba.Role)
                .IsRequired()
                .HasMaxLength(100); // Например, ограничение на длину строки роли

            builder.Property(ba => ba.DateAdded)
                .IsRequired(); // Дата добавления обязательна
        }
    }
}