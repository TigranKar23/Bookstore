namespace Bookstore.DAL.Models;

public class User : BaseModel
{
    public string UserName { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    
    public ICollection<BookUser> BookUsers { get; set; } // Связь с книгами
}