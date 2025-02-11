namespace Bookstore.DAL.Models
{
    public class BookUser : BaseModel
    {
        public string BookId { get; set; }
        public string UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }

}