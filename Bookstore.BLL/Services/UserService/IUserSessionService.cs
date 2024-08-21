using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System.Threading.Tasks;

namespace Bookstore.BLL.Services.UserService
{
    public interface IUserSessionService
    {
        Task<User> GetByToken(string token);
        Task<ResponseDto<UserSessionDto>> Login(UserLoginDto dto);
        Task<ResponseDto<bool>> Delete();
    }
}
