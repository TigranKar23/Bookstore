namespace Bookstore.DAL;
using Microsoft.EntityFrameworkCore;
using Bookstore.DAL.Models;
using Bookstore.DAL.Configurations;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<Error> Errors { get; set; }
    
    public DbSet<UserSession> UserSessions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Настройка связи многие ко многим между Book и User через BookUser
        modelBuilder.Entity<BookUser>()
            .HasKey(bu => new { bu.BookId, bu.UserId }); // Определяем составной ключ

        modelBuilder.Entity<BookUser>()
            .HasOne(bu => bu.Book)
            .WithMany(b => b.BookUsers)
            .HasForeignKey(bu => bu.BookId); // Внешний ключ на Book

        modelBuilder.Entity<BookUser>()
            .HasOne(bu => bu.User)
            .WithMany(u => u.BookUsers)
            .HasForeignKey(bu => bu.UserId); // Внешний ключ на User
        
        modelBuilder.Entity<BookAuthor>()
            .HasKey(ba => new { ba.BookId, ba.AuthorId });

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Book)
            .WithMany(b => b.BookAuthors)
            .HasForeignKey(ba => ba.BookId);

        modelBuilder.Entity<BookAuthor>()
            .HasOne(ba => ba.Author)
            .WithMany(a => a.BookAuthors)
            .HasForeignKey(ba => ba.AuthorId);
        
        modelBuilder.Entity<Error>(entity =>
        {
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Name).IsRequired();
        });

        modelBuilder.Entity<UserSession>(entity =>
        {
            // Конфигурация UserSession
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Token).IsRequired()
                .HasMaxLength(512); // Задайте размер в зависимости от ваших требований
            entity.Property(e => e.IsExpired).IsRequired();

            // Настройка связи с User
            entity.HasOne(e => e.User)
                .WithMany(u => u.UserSessions) // Если у User есть коллекция UserSessions
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Cascade); // Настройка поведения удаления
        });
        
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new BookUserConfiguration());
        modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
    }
    
}