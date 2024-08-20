using AutoMapper;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using StaffProjects.BLL.Constants;
using StaffProjects.BLL.Helpers;
using StaffProjects.DAL;
using StaffProjects.DAL.Models;
using StaffProjects.DTO;
using StaffProjects.DTO.UserDtos;
using System;
using System.Threading.Tasks;

namespace StaffProjects.BLL.Services.UserService
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
                FullName = dto.FullName,
                Email = dto.UserName.ToLower().Trim(),
                UserName = dto.UserName.ToLower().Trim(),
                PasswordHash = Crypto.HashPassword(dto.Password),
            };

            _db.Users.Add(newUser);

            await _db.SaveChangesAsync();

            response.Data = _mapper.Map<UserDto>(newUser);

            return response;
        }
    }
}
