namespace Bookstore.DAL.Models;
public class Book : BaseModel
{
        public string? Title { get; set; }
        
        public DateTime DateOfRelease { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        
        public ICollection<BookUser> BookUsers { get; set; } = new List<BookUser>();
        
}