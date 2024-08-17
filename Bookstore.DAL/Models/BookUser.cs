namespace Bookstore.DAL.Models
{
    public class BookUser : BaseModel
    {
        public long BookId { get; set; }
        public long UserId { get; set; }

        public Book Book { get; set; }
        public User User { get; set; }
    }

}