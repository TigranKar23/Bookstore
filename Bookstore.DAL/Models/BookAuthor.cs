namespace Bookstore.DAL.Models
{
    public class BookAuthor : BaseModel
    {
        public long BookId { get; set; }
        public long AuthorId { get; set; } // Измените на long, если Id в Author имеет тип long

        public string Role { get; set; } // Например, "Главный автор", "Редактор"
        public DateTime DateAdded { get; set; }

        public Book Book { get; set; }
        public Author Author { get; set; }
    }

}