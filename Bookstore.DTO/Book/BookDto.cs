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
        
        public int Count { get; set; }
        
        public bool IsAvailable { get; set; }
        public List<string> AuthorIds { get; set; }
    }
    
    public class ResponseMyBookDto: BaseDto
    {
        public string Title { get; set; }
        
        public DateTime DateOfRelease { get; set; }
        
        // public List<long> AuthorIds { get; set; }
    }
    
    public class BookDto
    {
        public string Title { get; set; }
        public DateTime DateOfRelease { get; set; }
        
        public int Count { get; set; }
        
        public bool IsAvailable { get; set; }
        public List<string> AuthorIds { get; set; } 
    }
    
    public class UpdateBookDto
    {
        public string? Title { get; set; }
        public DateTime? DateOfRelease { get; set; }
        
        public int? Count { get; set; }
        
        public bool? IsAvailable { get; set; }
        public List<string>? AuthorIds { get; set; } 
    }
    
    public class ResponseBooksListDto
    {
        public List<ResponseBookDto> Books { get; set; }
    }
    
    public class ResponseMyBooksListDto
    {
        public List<ResponseMyBookDto> Books { get; set; }
    }
}