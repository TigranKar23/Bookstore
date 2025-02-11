using Bookstore.BLL.Services.ErrorService;
using Bookstore.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bookstore.BLL.Helpers
{
    public class ErrorHelper
    {
        private readonly IErrorService _errorService;

        public ErrorHelper(IErrorService errorService)
        {
            _errorService = errorService;
        }

        public async Task<ResponseDto<T>> SetError<T>(ResponseDto<T> response, string id)
        {
            var error = await _errorService.GetById(id);

            response.Data = default;
            response.Errors = new List<ErrorModelDto>() { error };

            return response;
        }

        public async Task<ResponseDto<T>> SetError<T>(ResponseDto<T> response, ErrorModelDto error)
        {
            response.Data = default;
            response.Errors = new List<ErrorModelDto>() { error };

            return await Task.FromResult(response);
        }
    }
}
