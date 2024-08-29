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

        }

        [HttpPost("login")]
        public async Task<ResponseDto<UserSessionDto>> Login(UserLoginDto dto)
        {
            return await _userSessionService.Login(dto);
        }
        
        // [HttpPost("refresh")]
        // public IActionResult Refresh([FromBody] string refreshToken)
        // {
        //     if (IsValidRefreshToken(refreshToken))
        //     {
        //         var claims = new[]
        //         {
        //             new Claim(JwtRegisteredClaimNames.Sub, "username"), // замените на реальные данные
        //             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //         };
        //
        //         var accessToken = _jwtService.GenerateAccessToken(claims);
        //         var newRefreshToken = _jwtService.GenerateRefreshToken();
        //
        //         return Ok(new
        //         {
        //             AccessToken = accessToken,
        //             RefreshToken = newRefreshToken
        //         });
        //     }
        //
        //     return Unauthorized();
        // }

    }
}
