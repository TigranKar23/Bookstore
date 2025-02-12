    using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Bookstore.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Bookstore.DAL.Configurations
{
    public class UserConfiguration: IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            
            builder.HasMany(u => u.UserRoles)
                .WithOne(ur => ur.User)
                .HasForeignKey(bu => bu.RoleId);
            
            builder.HasMany(u => u.BookUsers)
                .WithOne(bu => bu.User)
                .HasForeignKey(bu => bu.UserId);
            
        }
    }
}