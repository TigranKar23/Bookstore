using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System.Threading.Tasks;

namespace Bookstore.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> Register(UserRegisterDto dto);
    }
}
