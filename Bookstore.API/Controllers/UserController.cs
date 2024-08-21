using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.BLL.Services.UserService;
using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System.Threading.Tasks;
using Bookstore.BLL.Services.UserService;

namespace Bookstore.API.Controllers
{
    [Route("api/user")]
    // [Authorize]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ResponseDto<UserDto>> Register(UserRegisterDto dto)
        {
            // Логирование для отладки
            Console.WriteLine("Register method called");

            return await _userService.Register(dto);
        }
        
    }
    
    

}