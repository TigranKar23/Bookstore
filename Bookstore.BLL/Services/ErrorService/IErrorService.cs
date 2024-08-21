using Bookstore.DTO;
using System.Threading.Tasks;

namespace Bookstore.BLL.Services.ErrorService
{
    public interface IErrorService
    {
        Task<ErrorModelDto> GetById(long id);
    }
}
