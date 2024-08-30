using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookstore.BLL.Constants;
using Bookstore.BLL.Helpers;
using Bookstore.DAL;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Bookstore.DTO.CategoryDtos;

namespace Bookstore.BLL.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ErrorHelper _errorHelpers;

        public CategoryService(AppDbContext db,
                             IMapper mapper, 
                             ErrorHelper errorHelpers)
        {
            _db = db;
            _mapper = mapper;
            _errorHelpers = errorHelpers;
        }

         public async Task<ResponseDto<CategoryDto>> CreateCategory(CreateCategoryDto dto)
    {
        var response = new ResponseDto<CategoryDto>();
        var category = new Category
        {
            Name = dto.Name,
            ParentCategoryId = dto.ParentCategoryId
        };

        _db.Categories.Add(category);
        await _db.SaveChangesAsync();

        response.Data = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId
        };

        return response;
    }

    public async Task<ResponseDto<CategoryDto>> UpdateCategory(UpdateCategoryDto dto)
    {
        var response = new ResponseDto<CategoryDto>();
        var category = await _db.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == dto.Id);

        if (category == null)
        {
            throw new Exception("Категория не найдена");
        }

        category.Name = dto.Name;
        category.ParentCategoryId = dto.ParentCategoryId;

        await _db.SaveChangesAsync();

        response.Data = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId,
            Subcategories = category.Subcategories.Select(sub => new CategoryDto
            {
                Id = sub.Id,
                Name = sub.Name,
                ParentCategoryId = sub.ParentCategoryId
            }).ToList()
        };

        return response;
    }
    
    public async Task<ResponseDto<CategoryDto>> GetCategoryById(int id)
    {
        var response = new ResponseDto<CategoryDto>();

        var category = await _db.Categories
            .Include(c => c.Subcategories)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return await _errorHelpers.SetError(response, ErrorConstants.ItemNotFound);
        }

        response.Data = new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId,
            Subcategories = category.Subcategories.Select(sub => new CategoryDto
            {
                Id = sub.Id,
                Name = sub.Name,
                ParentCategoryId = sub.ParentCategoryId
            }).ToList()
        };

        return response;
    }
    public async Task<ResponseDto<List<CategoryDto>>> GetAllCategories()
    {
        var response = new ResponseDto<List<CategoryDto>>();
        var categories = await _db.Categories
            .Include(c => c.Subcategories)
            .ToListAsync();

        response.Data = categories.Select(category => new CategoryDto
        {
            Id = category.Id,
            Name = category.Name,
            ParentCategoryId = category.ParentCategoryId,
            Subcategories = category.Subcategories.Select(sub => new CategoryDto
            {
                Id = sub.Id,
                Name = sub.Name,
                ParentCategoryId = sub.ParentCategoryId
            }).ToList()
        }).ToList();

        return response;
    }

    public async Task<ResponseDto<bool>> DeleteCategory(int id)
    {
        var response = new ResponseDto<bool>();

        var category = await _db.Categories
            .FirstOrDefaultAsync(c => c.Id == id);

        if (category == null)
        {
            return await _errorHelpers.SetError(response, ErrorConstants.ItemNotFound);
        }

        _db.Categories.Remove(category);
        await _db.SaveChangesAsync();

        response.Data = true;
        return response;
    }
    }
}
