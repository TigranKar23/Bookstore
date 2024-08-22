using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.DTO.BookDtos
{
    public class ResponseBookDto : BaseAdminDtoWithDate
    {
        public string Title { get; set; }
        public DateTime DateOfRelease { get; set; }
        public List<long> AuthorIds { get; set; } 
    }
    
    public class BookDto
    {
        public string Title { get; set; }
        public DateTime DateOfRelease { get; set; }
        public List<long> AuthorIds { get; set; } 
    }
    
    public class ResponseBooksListDto
    {
        public List<ResponseBookDto> Books { get; set; }
    }
}