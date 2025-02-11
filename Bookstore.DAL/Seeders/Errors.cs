using System;
using System.Linq;
using System.Threading.Tasks;
using Bookstore.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Bookstore.DAL.Seed
{
    public class ErrorSeeder
    {
        public static async Task SeedAsync(DbContext dbContext)
        {
            if (dbContext is AppDbContext context)
            {
                if (!context.Errors.Any())
                {
                    var errors = new[]
                    {
                        new Error { Id = Guid.NewGuid().ToString(), Name = "General Error", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "Unauthorized", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "Wrong Authorization Token", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "User Not Found", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "Incorrect Entered Data", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "Item Not Found", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "Duplicate Item", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "Email In Use", CreatedDate = DateTime.UtcNow },
                        new Error { Id = Guid.NewGuid().ToString(), Name = "Book Is Over", CreatedDate = DateTime.UtcNow },
                    };

                    await context.Errors.AddRangeAsync(errors);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}