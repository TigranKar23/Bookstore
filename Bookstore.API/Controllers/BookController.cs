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
        
        
        [Authorize]
        [HttpGet("getMyBooks")]
        public async Task<ResponseDto<ResponseMyBooksListDto>> GetMyBooks()
        {
            
            string strId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long userId = long.Parse(strId);
            return await _bookService.GetMyBooks(userId);
        }
        
        [Authorize]
        [HttpGet("byBook/{id}")]
        public async Task<ResponseDto<ResponseBookDto>> ByBook([FromRoute] long id)
        {
            
            var strId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            long userId = long.Parse(strId);
            return await _bookService.ByBook(id, userId);
        }
        
    }

}