namespace Bookstore.DAL.Models;

public class Author : BaseModel
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Biography { get; set; }
    public string Nationality { get; set; }
    public string Website { get; set; }
    
    public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}