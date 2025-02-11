namespace Bookstore.DAL;
using Microsoft.EntityFrameworkCore;
using Bookstore.DAL.Models;
using Bookstore.DAL.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;


public class AppDbContext : IdentityDbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public override int SaveChanges()
    {
        AddModificationDate();
        return base.SaveChanges();
    }
    
    public DbSet<Error> Errors { get; set; }
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRoles { get; set; }
    // public DbSet<Role> Users { get; set; }
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<BookAuthor> BookAuthors { get; set; }
    public DbSet<BookUser> BookUsers { get; set; }
    
    public DbSet<Category> Categories { get; set; }
    
    public DbSet<BookCategory> BookCategories { get; set; }
    
    private void AddModificationDate()
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.Entity is BaseModel && (
                e.State == EntityState.Added
                || e.State == EntityState.Modified));

        foreach (var entityEntry in entries)
        {
            ((BaseModel)entityEntry.Entity).Id = Guid.NewGuid().ToString();
            ((BaseModel)entityEntry.Entity).CreatedDate = DateTime.UtcNow;

            if (entityEntry.State == EntityState.Added)
            {
                ((BaseModel)entityEntry.Entity).CreatedDate = DateTime.UtcNow;
            }
        }
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        
        
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<User>()
        //     .Property(u => u.UserRole)
        //     .HasConversion(
        //         v => v.ToString(), // Сохранение как строка
        //         v => (User.UserRole)Enum.Parse(typeof(User.UserRole), v));
        
        modelBuilder.Entity<User>().ToTable("Users");
        modelBuilder.Entity<Role>().ToTable("Roles");
        modelBuilder.Entity<UserRole>().ToTable("UserRoles");
        
        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

        modelBuilder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);

        modelBuilder.Entity<BookUser>()
            .HasKey(bu => new { bu.BookId, bu.UserId });

        modelBuilder.Entity<BookUser>()
            .HasOne(bu => bu.Book)
            .WithMany(b => b.BookUsers)
            .HasForeignKey(bu => bu.BookId);

        modelBuilder.Entity<BookUser>()
            .HasOne(bu => bu.User)
            .WithMany(u => u.BookUsers)
            .HasForeignKey(bu => bu.UserId);
        
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

        modelBuilder.Entity<BookCategory>()
            .HasKey(bc => new { bc.BookId, bc.CategoryId });

        modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Book)
            .WithMany(b => b.BookCategories)
            .HasForeignKey(bc => bc.BookId);

        modelBuilder.Entity<BookCategory>()
            .HasOne(bc => bc.Category)
            .WithMany(c => c.BookCategories)
            .HasForeignKey(bc => bc.CategoryId);

        modelBuilder.Entity<Category>()
            .HasOne(c => c.ParentCategory)
            .WithMany(c => c.Subcategories)
            .HasForeignKey(c => c.ParentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        
        modelBuilder.ApplyConfiguration(new BookConfiguration());
        modelBuilder.ApplyConfiguration(new UserConfiguration());
        modelBuilder.ApplyConfiguration(new BookUserConfiguration());
        modelBuilder.ApplyConfiguration(new BookAuthorConfiguration());
    }
    
}