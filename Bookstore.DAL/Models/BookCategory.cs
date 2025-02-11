namespace Bookstore.DAL.Models
{
    public class BookCategory: BaseModel
    {
        public string BookId { get; set; }
        public Book Book { get; set; }

        public string CategoryId { get; set; }
        public Category Category { get; set; }
    }
}