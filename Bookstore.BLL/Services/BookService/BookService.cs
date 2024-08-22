using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Bookstore.BLL.Constants;
using Bookstore.BLL.Helpers;
using Bookstore.DAL;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using Bookstore.DTO.BookDtos;

namespace Bookstore.BLL.Services.BookService
{
    public class BookService : IBookService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ErrorHelper _errorHelpers;

        public BookService(AppDbContext db,
                             IMapper mapper, 
                             ErrorHelper errorHelpers)
        {
            _db = db;
            _mapper = mapper;
            _errorHelpers = errorHelpers;
        }

        public async Task<ResponseDto<ResponseBooksListDto>> GetAll(SearchDto dto)
        {
            List<Book> books;
            var response = new ResponseDto<ResponseBooksListDto>();
            
            if (string.IsNullOrEmpty(dto.Search))
            {
                books = await _db.Books.ToListAsync();
            }
            else
            {
                var searchTerm = dto.Search.ToLower().Trim();
                books = await _db.Books
                    .Where(x => x.Title.ToLower().Trim().Contains(searchTerm))
                    .Include(b => b.BookAuthors) // Включаем связанные BookAuthor
                    .ThenInclude(ba => ba.Author) // Включаем связанных Author через BookAuthor
                    .ToListAsync();
            }

            var bookDtos = books.Select(book => new ResponseBookDto
            {
                Title = book.Title,
                DateOfRelease = book.DateOfRelease,
                AuthorIds = book.BookAuthors
                    .Select(ba => ba.AuthorId) // Извлекаем только идентификаторы авторов
                    .ToList()
            }).ToList();

            response.Data = new ResponseBooksListDto
            {
                Books = bookDtos
            };
            
            return response;
        }

        public async Task<ResponseDto<ResponseBookDto>> GetOne(BaseDto dto)
        {
            var response = new ResponseDto<ResponseBookDto>();
            var book = await _db.Books.FindAsync(dto.Id);
            if (book == null)
            {
                return await _errorHelpers.SetError(response, ErrorConstants.ItemNotFound);
            }
            response.Data = _mapper.Map<ResponseBookDto>(book);

            return response;
        }   
        
        public async Task<ResponseDto<ResponseBookDto>> CreateBook(BookDto dto)
        {
            var response = new ResponseDto<ResponseBookDto>();

            // Проверка на уникальность названия книги (если необходимо)
            if (await _db.Books.AnyAsync(b => b.Title.ToLower().Trim() == dto.Title.ToLower().Trim()))
            {
                return await _errorHelpers.SetError(response, ErrorConstants.DuplicateItem);
            }

            // Создание новой книги
            var newBook = new Book
            {
                Title = dto.Title,
                DateOfRelease = dto.DateOfRelease
            };

            _db.Books.Add(newBook);

            if (dto.AuthorIds != null && dto.AuthorIds.Any())
            {
                var bookAuthors = dto.AuthorIds.Select(authorId => new BookAuthor
                {
                    BookId = newBook.Id,
                    AuthorId = authorId,
                    Role = "Автор", // Задайте роль по умолчанию или получите из DTO, если есть
                    DateAdded = DateTime.UtcNow
                }).ToList();

                _db.BookAuthors.AddRange(bookAuthors);
            }

            // Сохранение изменений
            await _db.SaveChangesAsync();

            // Преобразование созданной книги в DTO
            var bookDto = new ResponseBookDto
            {
                Title = newBook.Title,
                DateOfRelease = newBook.DateOfRelease,
                AuthorIds = dto.AuthorIds
            };

            response.Data = bookDto;

            return response;
        }

    }
}
