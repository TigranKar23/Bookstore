namespace Bookstore.DAL.Models;

public class User : BaseModel
{
    public enum UserRole
    {
        User,
        Admin
    }

    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public UserRole Role { get; set; } = UserRole.User;
    public string? RefreshToken { get; set; } = null;
    
    public ICollection<BookUser> BookUsers { get; set; }
    
    public ICollection<UserSession> UserSessions { get; set; }

}