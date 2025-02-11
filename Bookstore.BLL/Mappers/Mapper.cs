using AutoMapper;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using Bookstore.DTO.BookDtos;
using Bookstore.DTO.UserDtos;

namespace Bookstore.BLL.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Error, ErrorModelDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));

            CreateMap<User, UserDto>();
            
            CreateMap<UserDto, User>();
            
            CreateMap<ResponseAuthorDto, Author>();
            
            CreateMap<Author, ResponseAuthorDto>();

            CreateMap<Author, ResponseAuthorDto>()
                .ForMember(dest => dest.BookAuthors, opt => opt.MapFrom(src => src.BookAuthors));

            CreateMap<BookAuthor, ResponseBookAuthorDto>()
                .ForMember(dest => dest.Book, opt => opt.MapFrom(src => src.Book));

            CreateMap<Book, ResponseBookDto>();
            
        }
    }
}