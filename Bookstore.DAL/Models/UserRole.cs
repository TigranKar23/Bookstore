using Microsoft.AspNetCore.Identity;

namespace Bookstore.DAL.Models;

public class UserRole : IdentityUserRole<string>
{
    
    // public string RoleId { get; set; }
    // public string UserId { get; set; }
    
    // public User User { get; set; }
    // public Role Role { get; set; }
    
    public virtual User User { get; set; }
    public virtual Role Role { get; set; }
    
}