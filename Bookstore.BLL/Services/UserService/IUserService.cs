using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System.Threading.Tasks;
using Bookstore.DAL.Models;

namespace Bookstore.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> Register(UserRegisterDto dto);
        Task<User?> GetUserByIdAsync(long userId);
    }
}
