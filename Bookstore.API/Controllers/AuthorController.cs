using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.BLL.Services.AuthorService;
using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using System.Threading.Tasks;

namespace Bookstore.API.Controllers
{
    [Route("api/author")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [Authorize]
        [ServiceFilter(typeof(AdminRoleAttribute))]
        [HttpPost("create")]
        public async Task<ResponseDto<ResponseAuthorDto>> Register(AuthorDto dto)
        {
            return await _authorService.CreateAuthor(dto);
        }
        
        [Authorize]
        [HttpPost("getAll")]
        public async Task<ResponseDto<ResponseAuthorsListDto>> getAll(SearchDto dto)
        {
            return await _authorService.getAll(dto);
        }
        
        [AllowAnonymous]
        [HttpGet("getOne/{id}")]
        public async Task<ResponseDto<ResponseAuthorDto>> getOne([FromRoute] BaseDto dto)
        {
            return await _authorService.getOne(dto);
        }
        
    }
    
    

}