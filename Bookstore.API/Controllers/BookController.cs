    using System.Security.Claims;
    using System.Text.Json;
    using System.Text.Json.Serialization;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Bookstore.BLL.Services.AuthorService;
    using Bookstore.DTO;
    using Bookstore.DTO.AuthorDtos;
    using System.Threading.Tasks;
    using Bookstore.BLL.Services.BookService;
    using Bookstore.DAL.Models;
    using Bookstore.DTO.BookDtos;

    namespace Bookstore.API.Controllers
    {
        [Route("api/book")]
        [ApiController]
        public class BookController : ControllerBase
        {
            private readonly IBookService _bookService;

            public BookController(IBookService bookService)
            {
                _bookService = bookService;
            }

            [Authorize]
            [ServiceFilter(typeof(AdminRoleAttribute))]
            [HttpPost("create")]
            public async Task<ResponseDto<ResponseBookDto>> CreateBook(BookDto dto)
            {
                return await _bookService.CreateBook(dto);
            }
            
            [Authorize]
            [ServiceFilter(typeof(AdminRoleAttribute))]
            [HttpPost("update")]
            public async Task<ResponseDto<ResponseBookDto>> UpdateBook(UpdateBookDto dto)
            {
                return await _bookService.UpdateBook(dto);
            }
            
            [Authorize]
            [ServiceFilter(typeof(AdminRoleAttribute))] 
            [HttpPost("getAll")]
            public async Task<ResponseDto<ResponseBooksListDto>> GetAll(SearchDto dto)
            {
                string roleString = User.FindFirst(ClaimTypes.Role)?.Value;

                return await _bookService.GetAll(dto.Search, roleString);
            }
            
            [AllowAnonymous]
            [HttpGet("getOne/{id}")]
            public async Task<ResponseDto<ResponseBookDto>> GetOne([FromRoute] BaseDto dto)
            {
                return await _bookService.GetOne(dto);
            }
            
            
            [Authorize]
            [HttpGet("getMyBooks")]
            public async Task<ResponseDto<ResponseMyBooksListDto>> GetMyBooks()
            {
                
                string strId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return await _bookService.GetMyBooks(strId);
            }
            
            [Authorize]
            [HttpGet("byBook/{id}")]
            public async Task<ResponseDto<ResponseMyBookDto>> ByBook([FromRoute] string id)
            {
                
                var strId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                return await _bookService.ByBook(id, strId);
            }
            
        }

    }