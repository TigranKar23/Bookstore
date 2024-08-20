using StaffProjects.DAL.Models;
using StaffProjects.DTO;
using StaffProjects.DTO.UserDtos;
using System.Threading.Tasks;

namespace StaffProjects.BLL.Services.UserService
{
    public interface IUserSessionService
    {
        Task<User> GetByToken(string token);
        Task<ResponseDto<UserSessionDto>> Login(LoginDto dto);
        Task<ResponseDto<bool>> Delete();
    }
}
