using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DTO.BookDtos;

namespace Bookstore.DTO.AuthorDtos
{
    public class AuthorDto
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long Biography { get; set; }
        public string Nationality { get; set; }
        public string Website { get; set; }
    }
    
    public class ResponseAuthorDto : BaseAdminDtoWithDate
    {
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public long Biography { get; set; }
        public string Nationality { get; set; }
        public string Website { get; set; }
        
        public List<ResponseBookAuthorDto> BookAuthors { get; set; }
    }
    
    public class ResponseBookAuthorDto
    {
        public long BookId { get; set; }
        public string Role { get; set; }
        public DateTime DateAdded { get; set; }

        public ResponseBookDto Book { get; set; }
    }
    public class ResponseAuthorsListDto
    {
        public List<ResponseAuthorDto> Authors { get; set; }
    }
}