using StaffProjects.DTO;
using System.Threading.Tasks;

namespace StaffProjects.BLL.Services.ErrorService
{
    public interface IErrorService
    {
        Task<ErrorModelDto> GetById(long id);
    }
}
