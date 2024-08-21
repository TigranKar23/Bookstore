﻿using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Bookstore.DAL;
using System.Threading.Tasks;

namespace Bookstore.BLL.Helpers
{
    public static class MigrationHelper
    {
        public static async Task DatabaseMigrate(this WebApplication app)
        {
            await using var scope = app.Services.CreateAsyncScope();
            using var db = scope.ServiceProvider.GetService<AppDbContext>();
            await db.Database.MigrateAsync();
        }
    }
}
