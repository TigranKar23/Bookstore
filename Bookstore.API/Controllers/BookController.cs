using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.BLL.Services.AuthorService;
using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using System.Threading.Tasks;
using Bookstore.BLL.Services.BookService;
using Bookstore.DTO.BookDtos;

namespace Bookstore.API.Controllers
{
    [Route("api/book")]
    // [Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [AllowAnonymous]
        [HttpPost("create")]
        public async Task<ResponseDto<ResponseBookDto>> Register(BookDto dto)
        {
            return await _bookService.CreateBook(dto);
        }
        
        [AllowAnonymous]
        [HttpPost("getAll")]
        public async Task<ResponseDto<ResponseBooksListDto>> GetAll(SearchDto dto)
        {
            return await _bookService.GetAll(dto);
        }
        
        [AllowAnonymous]
        [HttpGet("getOne/{id}")]
        public async Task<ResponseDto<ResponseBookDto>> GetOne([FromRoute] BaseDto dto)
        {
            return await _bookService.GetOne(dto);
        }
        
    }
    
    

}