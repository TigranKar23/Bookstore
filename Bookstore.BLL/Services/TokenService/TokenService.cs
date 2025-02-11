using Bookstore.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

public class TokenService
{
    private readonly UserManager<User> _userManager;

    public TokenService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }

    public async Task<string> GenerateTokenAsync(User user, string tokenName)
    {
        var token = await _userManager.GenerateUserTokenAsync(user, TokenOptions.DefaultProvider, tokenName);
        await _userManager.SetAuthenticationTokenAsync(user, TokenOptions.DefaultProvider, tokenName, token);
        return token;
    }
    
    public async Task<bool> ValidateTokenAsync(User user, string tokenName, string token)
    {
        return await _userManager.VerifyUserTokenAsync(user, TokenOptions.DefaultProvider, tokenName, token);
    }
}