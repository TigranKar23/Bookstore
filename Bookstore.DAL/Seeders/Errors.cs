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
                    var currentDateTime = DateTime.Now;

                    var errors = new[]
                    {
                        new Error { Id = 1, Name = "General Error", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 2, Name = "Unauthorized", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 3, Name = "Wrong Authorization Token", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 4, Name = "User Not Found", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 5, Name = "Incorrect Entered Data", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 6, Name = "Item Not Found", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 7, Name = "Duplicate Item", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 8, Name = "Email In Use", CreatedDate = DateTime.UtcNow },
                        new Error { Id = 9, Name = "Book Is Over", CreatedDate = DateTime.UtcNow },
                    };

                    await context.Errors.AddRangeAsync(errors);
                    await context.SaveChangesAsync();
                }
            }
        }
    }
}