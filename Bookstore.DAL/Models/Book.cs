using System.Runtime.InteropServices.JavaScript;

namespace Bookstore.DAL.Models;
public class Book : BaseModel
{
        public string? Title { get; set; }
        
        public DateTime DateOfRelease { get; set; }
        
        public int Count { get; set; }
        
        public bool IsAvailable { get; set; }
        public ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
        
        public ICollection<BookUser> BookUsers { get; set; } = new List<BookUser>();
        
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
        
}