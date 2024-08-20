using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DTO.UserDtos
{
    public class BookDto : BaseAdminDtoWithDate
    {
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}