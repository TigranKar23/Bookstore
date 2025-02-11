using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Bookstore.BLL.Services.CategoryService;
using Bookstore.DTO;
using Bookstore.DTO.CategoryDtos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Bookstore.API.Controllers
{
    [Route("api/category")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [Authorize]
        [ServiceFilter(typeof(AdminRoleAttribute))]
        [HttpPost("create")]
        public async Task<ResponseDto<CategoryDto>> CreateCategory(CreateCategoryDto dto)
        {
            return await _categoryService.CreateCategory(dto);
        }

        [Authorize]
        [ServiceFilter(typeof(AdminRoleAttribute))]
        [HttpPost("update")]
        public async Task<ResponseDto<CategoryDto>> UpdateCategory(UpdateCategoryDto dto)
        {
            return await _categoryService.UpdateCategory(dto);
        }

        [AllowAnonymous]
        [HttpGet("getAll")]
        public async Task<ResponseDto<List<CategoryDto>>> GetAllCategories()
        {
            return await _categoryService.GetAllCategories();
        }

        [Authorize]
        [ServiceFilter(typeof(AdminRoleAttribute))]
        [HttpDelete("delete/{id}")]
        public async Task<ResponseDto<bool>> DeleteCategory([FromRoute] string id)
        {
            return await _categoryService.DeleteCategory(id);
        }

        [AllowAnonymous]
        [HttpGet("getOne/{id}")]
        public async Task<ResponseDto<CategoryDto>> GetCategoryById([FromRoute] string id)
        {
            return await _categoryService.GetCategoryById(id);
        }
    }
}