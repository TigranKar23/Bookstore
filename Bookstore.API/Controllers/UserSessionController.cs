using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Bookstore.BLL.Services.UserService;
using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System.Threading.Tasks;

namespace Bookstore.API.Controllers
{
    [Route("api/user-session")]
    [ApiController]
    public class UserSessionController : ControllerBase
    {
        
        private readonly IUserSessionService _userSessionService;

        public UserSessionController(IUserSessionService userSessionService)
        {
            _userSessionService = userSessionService;
            Console.WriteLine("1111111111111111111111111111111111");

        }

        [HttpPost("login")]
        public async Task<ResponseDto<UserSessionDto>> Login(UserLoginDto dto)
        {
            Console.WriteLine("1111111111111111111111111111111111");

            return await _userSessionService.Login(dto);
        }
    }
}
