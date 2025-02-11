namespace Bookstore.DAL.Models
{
    public class BookAuthor : BaseModel
    {
        public string BookId { get; set; }
        public string AuthorId { get; set; }

        public string Role { get; set; }
        public DateTime DateAdded { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }

}