using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using System.Threading.Tasks;
using Bookstore.DTO.BookDtos;

namespace Bookstore.BLL.Services.BookService
{
    public interface IBookService
    {
        Task<ResponseDto<ResponseBookDto>> CreateBook(BookDto dto);
        Task<ResponseDto<ResponseBooksListDto>> GetAll(SearchDto dto);
        Task<ResponseDto<ResponseBookDto>> GetOne(BaseDto dto);
    }
}
