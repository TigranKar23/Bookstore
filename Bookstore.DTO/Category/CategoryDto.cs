namespace Bookstore.DTO.CategoryDtos;
public class CreateCategoryDto
{
    public string Name { get; set; }
    public int? ParentCategoryId { get; set; }
}

public class UpdateCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public long? ParentCategoryId { get; set; }
}

public class CategoryDto: BaseDto
{
    public string Name { get; set; }
    public long? ParentCategoryId { get; set; }
    public List<CategoryDto> Subcategories { get; set; } = new List<CategoryDto>();
}