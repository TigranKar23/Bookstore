using Microsoft.AspNetCore.Identity;

namespace Bookstore.DAL.Models;

public class User : IdentityUser
{

    // public string UserName { get; set; }
    // public string Password { get; set; }
    // public string Email { get; set; }
    
    
    public string? RefreshToken { get; set; } = null;
    
    public ICollection<UserRole> UserRoles { get; set; }
    
    public ICollection<BookUser> BookUsers { get; set; }
    
}