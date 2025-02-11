using Bookstore.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class TokenController : ControllerBase
{
    private readonly TokenService _tokenService;
    private readonly UserManager<User> _userManager;

    public TokenController(TokenService tokenService, UserManager<User> userManager)
    {
        _tokenService = tokenService;
        _userManager = userManager;
    }

    [HttpPost("generate")]
    public async Task<IActionResult> GenerateToken([FromBody] string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return NotFound("User not found");

        var token = await _tokenService.GenerateTokenAsync(user, "CustomToken");
        return Ok(new { Token = token });
    }

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateToken([FromBody] TokenRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        if (user == null) return NotFound("User not found");

        var isValid = await _tokenService.ValidateTokenAsync(user, "CustomToken", request.Token);
        return Ok(new { IsValid = isValid });
    }
}

public class TokenRequest
{
    public string Email { get; set; }
    public string Token { get; set; }
}