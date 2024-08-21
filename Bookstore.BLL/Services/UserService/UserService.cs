using AutoMapper;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Bookstore.BLL.Constants;
using Bookstore.BLL.Helpers;
using Bookstore.DAL;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System;
using System.Threading.Tasks;
using Bookstore.BLL.Services.UserService;

namespace Bookstore.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ErrorHelper _errorHelpers;
        private readonly IUserSessionService _userSessionService;

        public UserService(AppDbContext db,
                           IMapper mapper, 
                           ErrorHelper errorHelpers, 
                           IUserSessionService userSessionService)
        {
            _db = db;
            _mapper = mapper;
            _errorHelpers = errorHelpers;
            _userSessionService = userSessionService;
        }

        public async Task<ResponseDto<UserDto>> Register(UserRegisterDto dto)
        {
            var response = new ResponseDto<UserDto>();

            if (await _db.Users.AnyAsync(x => x.Email == dto.UserName.ToLower().Trim()))
            {
                return await _errorHelpers.SetError(response, ErrorConstants.EmailInUse);
            }

            var newUser = new User()
            {
                Email = dto.UserName.ToLower().Trim(),
                UserName = dto.UserName.ToLower().Trim(),
                Password = Crypto.HashPassword(dto.Password),
                CreatedDate = DateTime.UtcNow
            };

            _db.Users.Add(newUser);

            await _db.SaveChangesAsync();

            response.Data = _mapper.Map<UserDto>(newUser);

            return response;
        }
    }
}
