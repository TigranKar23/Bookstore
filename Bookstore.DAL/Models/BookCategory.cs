namespace Bookstore.DAL.Models
{
    public class BookCategory: BaseModel
    {
        public long BookId { get; set; }
        public Book Book { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }
}