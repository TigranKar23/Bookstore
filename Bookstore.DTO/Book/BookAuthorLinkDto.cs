using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DTO.UserDtos
{
    public class BookDto : BaseAdminDtoWithDate
    {
        public string Title { get; set; }
        public DateTime DateOfRelease { get; set; }
        public List<long> AuthorIds { get; set; } 
    }
}