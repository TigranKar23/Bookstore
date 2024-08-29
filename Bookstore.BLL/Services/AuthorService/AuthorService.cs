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

        public async Task<ResponseDto<ResponseAuthorsListDto>> getAll(SearchDto dto)
        {
            List<Author> authors;

            if (string.IsNullOrEmpty(dto.Search))
            {
                authors = await _db.Authors.ToListAsync();
            }
            else
            {
                var searchTerm = dto.Search.ToLower().Trim();
                authors = await _db.Authors
                    .Where(x => x.LastName.ToLower().Trim().Contains(searchTerm) ||
                                x.FirstName.ToLower().Trim().Contains(searchTerm))
                    .ToListAsync();
            }

            var authorDtos = authors.Select(author => new ResponseAuthorDto
            {
                LastName = author.LastName,
                FirstName = author.FirstName,
                DateOfBirth = author.DateOfBirth,
                Biography = author.Biography,
                Nationality = author.Nationality,
                Website = author.Website,
                Id = author.Id
            }).ToList();

            var response = new ResponseDto<ResponseAuthorsListDto>
            {
                Data = new ResponseAuthorsListDto
                {
                    Authors = authorDtos
                }
            };

            return response;
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

            var newAuthor = new Author
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

        public async Task<ResponseDto<ResponseAuthorDto>> getOne(BaseDto dto)
        {
            var response = new ResponseDto<ResponseAuthorDto>();

            var author = await _db.Authors
                .Include(a => a.BookAuthors)
                .ThenInclude(ba => ba.Book)
                .FirstOrDefaultAsync(a => a.Id == dto.Id);

            if (author == null)
            {
                return await _errorHelpers.SetError(response, ErrorConstants.ItemNotFound);
            }

            
                
            response.Data = _mapper.Map<ResponseAuthorDto>(author);
            
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.IgnoreCycles
            };

            string authorJson = JsonSerializer.Serialize(author, options);
            Console.WriteLine("authorauthorauthorauthorauthorauthor");
            Console.WriteLine(authorJson);
            

            return response;
        }
    }
}
