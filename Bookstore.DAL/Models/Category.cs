namespace Bookstore.DAL.Models
{
    public class Category : BaseModel
    {
        public string Name { get; set; }
        
        public long? ParentCategoryId { get; set; }
        public Category? ParentCategory { get; set; }
        public ICollection<Category> Subcategories { get; set; } = new List<Category>();
        public ICollection<BookCategory> BookCategories { get; set; } = new List<BookCategory>();
    }
}