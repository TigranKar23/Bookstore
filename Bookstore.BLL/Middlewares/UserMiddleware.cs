using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bookstore.BLL.Services.UserService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

public class UserMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceScopeFactory _serviceScopeFactory;

    public UserMiddleware(RequestDelegate next, IServiceScopeFactory serviceScopeFactory)
    {
        _next = next;
        _serviceScopeFactory = serviceScopeFactory;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine("0000000000000000000000000000000000000");
        
        using (var scope = _serviceScopeFactory.CreateScope())
        {
            var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
            
            

            var userIdClaim = context.User.FindFirst("user_id")?.Value;
            
            

            if (!string.IsNullOrEmpty(userIdClaim) && long.TryParse(userIdClaim, out var userId))
            {
                var user = await userService.GetUserByIdAsync(userId);
                
                if (user != null)
                {
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.UserName),
                        new Claim(ClaimTypes.Email, user.Email),
                        new Claim(ClaimTypes.Role, user.Role.ToString())

                    };

                    var identity = new ClaimsIdentity(claims, "custom");
                    context.User = new ClaimsPrincipal(identity);
                }
            }
        }

        await _next(context);
    }
}