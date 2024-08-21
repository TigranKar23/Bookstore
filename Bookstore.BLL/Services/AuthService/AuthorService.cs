using AutoMapper;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Bookstore.BLL.Constants;
using Bookstore.BLL.Helpers;
using Bookstore.DAL;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System;
using System.Threading.Tasks;
using Bookstore.DTO.AuthorDtos;

namespace Bookstore.BLL.Services.AuthorService
{
    public class AuthorService : IAuthorService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ErrorHelper _errorHelpers;

        public AuthorService(AppDbContext db,
                           IMapper mapper, 
                           ErrorHelper errorHelpers)
        {
            _db = db;
            _mapper = mapper;
            _errorHelpers = errorHelpers;
        }

        public async Task<ResponseDto<ResponseAuthorDto>> CreateAuthor(AuthorDto dto)
        {
            var response = new ResponseDto<ResponseAuthorDto>();

            if (await _db.Authors.AnyAsync(x =>
                    x.LastName == dto.LastName.ToLower().Trim() &&
                    x.FirstName == dto.FirstName.ToLower().Trim()))
            {
                return await _errorHelpers.SetError(response, ErrorConstants.EmailInUse);
            }

            var newAuthor = new Author()
            {
                LastName = dto.LastName,
                FirstName = dto.FirstName,
                DateOfBirth = dto.DateOfBirth,
                Biography = dto.Biography,
                Nationality = dto.Nationality,
                Website = dto.Website,
                CreatedDate = DateTime.UtcNow
            };

            _db.Authors.Add(newAuthor);

            await _db.SaveChangesAsync();

            response.Data = _mapper.Map<ResponseAuthorDto>(newAuthor);

            return response;
        }
        
        public async Task<ResponseDto<ResponseAuthorsListDto>> getAll(SearchDto dto)
        {
            List<Author> authors;

            if (string.IsNullOrEmpty(dto.Search))
            {
                // If no search term is provided, return all authors
                authors = await _db.Authors.ToListAsync();
            }
            else
            {
                // If a search term is provided, filter authors by LastName or FirstName
                var searchTerm = dto.Search.ToLower().Trim();
                authors = await _db.Authors
                    .Where(x => x.LastName.ToLower().Trim().Contains(searchTerm) ||
                                x.FirstName.ToLower().Trim().Contains(searchTerm))
                    .ToListAsync();
            }

            // Map the authors to the AuthorDto
            var authorDtos = authors.Select(a => new ResponseAuthorDto
            {
                Id = a.Id,
                LastName = a.LastName,
                FirstName = a.FirstName,
                DateOfBirth = a.DateOfBirth,
                Biography = a.Biography,
                Nationality = a.Nationality,
                Website = a.Website
            }).ToList();

            var response = new ResponseAuthorsListDto
            {
                Authors = authorDtos
            };

            return new ResponseDto<ResponseAuthorsListDto>
            {
                Data = response,
                Errors = null // Handle errors if needed
            };
        }
    }
}
