using AutoMapper;
using CryptoHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Bookstore.BLL.Constants;
using Bookstore.BLL.Helpers;
using Bookstore.BLL.Models;
using Bookstore.DAL;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bookstore.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace Bookstore.BLL.Services.UserService
{
    public class UserSessionService : IUserSessionService
    {
        private readonly AppDbContext _db;
        private readonly ErrorHelper _errorHelper;
        private readonly AuthOptions _options;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly IConfiguration _configuration;
        private readonly JwtService _jwtService;

        public User CurrentUser { get; private set; }

        public UserSessionService(AppDbContext db,
                                  IOptions<AuthOptions> options,
                                  ErrorHelper errorHelper,
                                  IMapper mapper,
                                  IHttpContextAccessor httpContext,
                                  IConfiguration configuration, JwtService jwtService)
        {
            _db = db;
            _options = options.Value;
            _errorHelper = errorHelper;
            _mapper = mapper;
            _httpContext = httpContext;
            _configuration = configuration;
            _jwtService = jwtService;
        }

        public async Task<User> GetByToken(string token)
        {
            var userSession = await _db.UserSessions
                                            .Include(x => x.User)
                                            .FirstOrDefaultAsync(x => x.Token == token && !x.IsExpired
                                                                                       && x.User.IsDeleted != true);
            if (userSession == null)
            {
                return null;
            }

            if (userSession.ModifyDate.Value.AddMinutes(_options.TokenExpirationTimeInMinutes) < DateTime.Now)
            {
                userSession.IsExpired = true;

                await _db.SaveChangesAsync();

                return null;
            }

            CurrentUser = userSession.User;

            userSession.ModifyDate = DateTime.Now;

            await _db.SaveChangesAsync();

            return CurrentUser;
        }

        public async Task<ResponseDto<UserSessionDto>> Login(UserLoginDto dto)
        {
            var response = new ResponseDto<UserSessionDto>();
            var user = _db.Users.SingleOrDefault(u => u.UserName == dto.UserName);
            if (user == null || !Crypto.VerifyHashedPassword(user.Password, dto.Password))
            {
                return await _errorHelper.SetError(response, ErrorConstants.ItemNotFound);
            }

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, dto.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim("user_id", user.Id.ToString())
            };

            var accessToken = _jwtService.GenerateAccessToken(claims);
            var refreshToken = _jwtService.GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            // user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);
            _db.SaveChanges();
            
            var userSession = new UserSessionDto
            {
                UserId = user.Id,
                Token = accessToken,
                RefreshToken = refreshToken
            };

            response.Data = userSession;
            return response;

        }

        public async Task<ResponseDto<bool>> Delete()
        {
            throw new NotImplementedException();
        }
    }
}
