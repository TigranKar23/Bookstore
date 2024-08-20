using StaffProjects.DTO;
using StaffProjects.DTO.UserDtos;
using System.Threading.Tasks;

namespace StaffProjects.BLL.Services.UserService
{
    public interface IUserService
    {
        Task<ResponseDto<UserDto>> Register(UserRegisterDto dto);
    }
}
