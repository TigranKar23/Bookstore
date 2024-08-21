using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DTO.BookDtos
{
    public class BookUserLinkDto : BaseAdminDtoWithDate
    {
        public long BookId { get; set; }
        public long UserId { get; set; }
    }
}