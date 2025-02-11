using Microsoft.AspNetCore.Identity;

namespace Bookstore.DAL.Models;

public class UserRole : IdentityUserRole<string>
{
    
    // public Guid RoleId { get; set; }
    // public Guid UserId { get; set; }
    
    public User User { get; set; }
    public Role Role { get; set; }
    
}