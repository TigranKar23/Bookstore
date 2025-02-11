using Bookstore.DTO;
using Bookstore.DTO.CategoryDtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<ResponseDto<CategoryDto>> CreateCategory(CreateCategoryDto dto);
        Task<ResponseDto<List<CategoryDto>>> GetAllCategories();
        Task<ResponseDto<CategoryDto>> GetCategoryById(string id);
        Task<ResponseDto<CategoryDto>> UpdateCategory(UpdateCategoryDto dto);
        Task<ResponseDto<bool>> DeleteCategory(string id);
    }
}