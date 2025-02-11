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

        public async Task<ResponseDto<ResponseBooksListDto>> GetAll(string? Search, string Role)
        {
            List<Book> books;
            var response = new ResponseDto<ResponseBooksListDto>();
            bool filterByAvailability = Role.Equals("User", StringComparison.OrdinalIgnoreCase);
            
            if (string.IsNullOrEmpty(Search))
            {
                books = await _db.Books
                    .Where(b => !filterByAvailability || b.IsAvailable).Include(book => book.BookAuthors)
                    .ToListAsync();
            }
            else
            {
                var searchTerm = Search.ToLower().Trim();
                books = await _db.Books
                    .Where(x => x.Title.ToLower().Trim().Contains(searchTerm) && (!filterByAvailability || x.IsAvailable))
                    .Include(b => b.BookAuthors)
                    .ThenInclude(ba => ba.Author)
                    .ToListAsync();
            }

            var bookDtos = books.Select(book => new ResponseBookDto
            {
                Id = book.Id,
                Title = book.Title,
                DateOfRelease = book.DateOfRelease,
                IsAvailable = book.IsAvailable,
                Count = book.Count,
                AuthorIds = book.BookAuthors
                    .Select(ba => ba.AuthorId)
                    .ToList()
            }).ToList();

            response.Data = new ResponseBooksListDto
            {
                Books = bookDtos
            };
            
            return response;
        }
        
        public async Task<ResponseDto<ResponseMyBooksListDto>> GetMyBooks(string userId)
        {
            var response = new ResponseDto<ResponseMyBooksListDto>();

            var books = await _db.BookUsers
                .Where(bu => bu.UserId == userId)
                .Select(bu => bu.Book)
                .ToListAsync();

            var responseBooks = books.Select(book => new ResponseMyBookDto
            {
                Id = book.Id,
                Title = book.Title,
                DateOfRelease = book.DateOfRelease,
                // Authors = book.BookAuthors.Select(ba => ba.Author.Name).ToList() 
            }).ToList();

            response.Data = new ResponseMyBooksListDto
            {
                Books = responseBooks
            };
            return response;
        }
        
        public async Task<ResponseDto<ResponseMyBookDto>> ByBook(string Id, string userId)
        {
            var response = new ResponseDto<ResponseMyBookDto>();
            var book = await _db.Books.FindAsync(Id);
            if (book == null)
            {
                return await _errorHelpers.SetError(response, ErrorConstants.ItemNotFound);
            }

            if (book.Count < 1 || !book.IsAvailable)
            {
                return await _errorHelpers.SetError(response, ErrorConstants.BookIsOver);
            }
            else if(book.Count == 1)
            {
                book.IsAvailable = false;
            }
            book.Count--;
            
            var newBookUser = new BookUser
            {
                BookId = Id,
                UserId = userId,
                CreatedDate = DateTime.UtcNow
            };

            _db.BookUsers.Add(newBookUser);
            await _db.SaveChangesAsync();
            
            response.Data = _mapper.Map<ResponseMyBookDto>(book);
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

            if (await _db.Books.AnyAsync(b => b.Title.ToLower().Trim() == dto.Title.ToLower().Trim()))
            {
                return await _errorHelpers.SetError(response, ErrorConstants.DuplicateItem);
            }

            var newBook = new Book
            {
                Title = dto.Title,
                DateOfRelease = dto.DateOfRelease,
                IsAvailable = dto.IsAvailable,
                Count = dto.Count | 1,
            };

            _db.Books.Add(newBook);
            
            await _db.SaveChangesAsync();

            if (dto.AuthorIds != null && dto.AuthorIds.Any())
            {
                var bookAuthors = dto.AuthorIds.Select(authorId => new BookAuthor
                {
                    BookId = newBook.Id,
                    AuthorId = authorId,
                    Role = "Автор",
                    DateAdded = DateTime.UtcNow
                }).ToList();

                _db.BookAuthors.AddRange(bookAuthors);
            }

            await _db.SaveChangesAsync();

            var bookDto = new ResponseBookDto
            {
                Id = newBook.Id,
                Title = newBook.Title,
                DateOfRelease = newBook.DateOfRelease,
                IsAvailable = newBook.IsAvailable,
                Count = newBook.Count,
                AuthorIds = dto.AuthorIds
            };

            response.Data = bookDto;

            return response;
        }
        
        public async Task<ResponseDto<ResponseBookDto>> UpdateBook(UpdateBookDto dto)
        {
            var response = new ResponseDto<ResponseBookDto>();
    
            var book = await _db.Books
                .FirstOrDefaultAsync(b => b.Title.ToLower().Trim() == dto.Title.ToLower().Trim());

            if (book == null)
            {
                return await _errorHelpers.SetError(response, ErrorConstants.ItemNotFound);
            }

            if (dto.DateOfRelease.HasValue)
                book.DateOfRelease = dto.DateOfRelease.Value;
    
            if (dto.Count.HasValue)
                book.Count = dto.Count.Value;
    
            if (dto.IsAvailable.HasValue)
                book.IsAvailable = dto.IsAvailable.Value;
    
            _db.Books.Update(book);

            if (dto.AuthorIds != null && dto.AuthorIds.Any())
            {
                var existingAuthors = await _db.BookAuthors
                    .Where(ba => ba.BookId == book.Id)
                    .ToListAsync();

                _db.BookAuthors.RemoveRange(existingAuthors);

                var bookAuthors = dto.AuthorIds.Select(authorId => new BookAuthor
                {
                    BookId = book.Id,
                    AuthorId = authorId,
                    Role = "Автор",
                    DateAdded = DateTime.UtcNow
                }).ToList();

                _db.BookAuthors.AddRange(bookAuthors);
            }

            await _db.SaveChangesAsync();

            var bookDto = new ResponseBookDto
            {
                Id = book.Id,
                Title = book.Title,
                DateOfRelease = book.DateOfRelease,
                IsAvailable = book.IsAvailable,
                Count = book.Count,
                AuthorIds = dto.AuthorIds ?? new List<string>()
            };

            response.Data = bookDto;

            return response;
        }


    }
}
