using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using System.Threading.Tasks;
using Bookstore.DTO.BookDtos;

namespace Bookstore.BLL.Services.BookService
{
    public interface IBookService
    {
        Task<ResponseDto<ResponseBookDto>> CreateBook(BookDto dto);
        Task<ResponseDto<ResponseBookDto>> UpdateBook(UpdateBookDto dto);
        Task<ResponseDto<ResponseBooksListDto>> GetAll(string? Search, string Role);
        Task<ResponseDto<ResponseBookDto>> GetOne(BaseDto dto);
        Task<ResponseDto<ResponseMyBookDto>> ByBook(long Id, long userId);
        Task<ResponseDto<ResponseMyBooksListDto>> GetMyBooks(long userId);
    }
}
