using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

public class AuthorizationErrorHandlerMiddleware
{
    private readonly RequestDelegate _next;

    public AuthorizationErrorHandlerMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (UnauthorizedAccessException)
        {
            context.Response.StatusCode = StatusCodes.Status403Forbidden;
            await context.Response.WriteAsync("Authorization error: You are not authorized to access this resource.");
        }
    }
}

public static class AuthorizationErrorHandlerMiddlewareExtensions
{
    public static IApplicationBuilder UseAuthorizationErrorHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<AuthorizationErrorHandlerMiddleware>();
    }
}