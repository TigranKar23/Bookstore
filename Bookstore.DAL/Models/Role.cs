using Microsoft.AspNetCore.Identity;

namespace Bookstore.DAL.Models;

public class Role : IdentityRole
{
    
    public string? Description { get; set; }
    
    public ICollection<UserRole> UserRoles { get; set; }
    
}