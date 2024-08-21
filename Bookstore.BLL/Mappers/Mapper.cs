using AutoMapper;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.AuthorDtos;
using Bookstore.DTO.UserDtos;

namespace Bookstore.BLL.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Существующий маппинг
            CreateMap<Error, ErrorModelDto>()
                .ForMember(dest => dest.Code, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.Name));

            CreateMap<User, UserDto>();
            
            CreateMap<UserDto, User>();
            
            CreateMap<ResponseAuthorDto, Author>();
            
            CreateMap<Author, ResponseAuthorDto>();

            // Новый маппинг для UserSession и UserSessionDto
            CreateMap<UserSession, UserSessionDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
                .ForMember(dest => dest.Token, opt => opt.MapFrom(src => src.Token))
                .ForMember(dest => dest.IsExpired, opt => opt.MapFrom(src => src.IsExpired));
        }
    }
}