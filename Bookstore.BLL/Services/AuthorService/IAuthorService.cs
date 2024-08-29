using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using System.Threading.Tasks;

namespace Bookstore.BLL.Services.AuthorService
{
    public interface IAuthorService
    {
        Task<ResponseDto<ResponseAuthorDto>> CreateAuthor(AuthorDto dto);
        Task<ResponseDto<ResponseAuthorsListDto>> getAll(SearchDto dto);
        Task<ResponseDto<ResponseAuthorDto>> getOne(BaseDto dto);
    }
}
