using AutoMapper;
using CryptoHelper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using StaffProjects.BLL.Constants;
using StaffProjects.BLL.Helpers;
using StaffProjects.BLL.Models;
using StaffProjects.DAL;
using StaffProjects.DAL.Models;
using StaffProjects.DTO;
using StaffProjects.DTO.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StaffProjects.BLL.Services.UserService
{
    public class UserSessionService : IUserSessionService
    {
        private readonly AppDbContext _db;
        private readonly ErrorHelper _errorHelper;
        private readonly AuthOptions _options;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public User CurrentUser { get; private set; }

        public UserSessionService(AppDbContext db,
                                  IOptions<AuthOptions> options,
                                  ErrorHelper errorHelper,
                                  IMapper mapper,
                                  IHttpContextAccessor httpContext)
        {
            _db = db;
            _options = options.Value;
            _errorHelper = errorHelper;
            _mapper = mapper;
            _httpContext = httpContext;
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

        public async Task<ResponseDto<UserSessionDto>> Login(LoginDto dto)
        {
            var response = new ResponseDto<UserSessionDto>();

            var dbUser = await _db.Users
                                  .FirstOrDefaultAsync(x => x.UserName == dto.UserName.ToLower()
                                                                                       .Replace(" ", ""));

            if (dbUser == null || !Crypto.VerifyHashedPassword(dbUser.PasswordHash, dto.Password))
            {
                return await _errorHelper.SetError(response, ErrorConstants.IncorrectEnteredData);
            }

            var token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");

            var session = new UserSession()
            {
                Token = token,
                UserId = dbUser.Id
            };

            _db.UserSessions.Add(session);

            await _db.SaveChangesAsync();

            response.Data = _mapper.Map<UserSessionDto>(await _db.UserSessions
                                                                 .Include(x => x.User)
                                                                 .FirstOrDefaultAsync(x => x.Id == session.Id));
            return response;
        }

        public async Task<ResponseDto<bool>> Delete()
        {
            throw new NotImplementedException();
        }
    }
}
