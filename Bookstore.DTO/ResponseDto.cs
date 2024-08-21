using System.Collections.Generic;
using System.Linq;

namespace Bookstore.DTO
{
    public class ResponseDto<T>
    {
        public T Data { get; set; }
        public bool HasError => Errors != null && Errors.Any();
        public List<ErrorModelDto> Errors { get; set; }
    }
}
