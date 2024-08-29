using Bookstore.DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

public class AuthorizationFilter : IAsyncActionFilter
{
    private readonly AppDbContext _db;

    public AuthorizationFilter(AppDbContext db)
    {
        _db = db;
    }

    public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

        if (string.IsNullOrEmpty(token))
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userSession = await _db.UserSessions.Include(x => x.User)
            .FirstOrDefaultAsync(x => x.Token == token);

        if (userSession == null || userSession.CreatedDate.AddHours(24) < DateTime.UtcNow)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        context.HttpContext.Items["User"] = userSession.User;

        await next();
    }
}