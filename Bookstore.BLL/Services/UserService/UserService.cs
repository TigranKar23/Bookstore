﻿using AutoMapper;
using CryptoHelper;
using Microsoft.EntityFrameworkCore;
using Bookstore.BLL.Constants;
using Bookstore.BLL.Helpers;
using Bookstore.DAL;
using Bookstore.DAL.Models;
using Bookstore.DTO;
using Bookstore.DTO.UserDtos;

using Microsoft.AspNetCore.Identity; 

namespace Bookstore.BLL.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly AppDbContext _db;
        private readonly IMapper _mapper;
        private readonly ErrorHelper _errorHelpers;
        
        private readonly UserManager<User> _userManager; 
        private readonly RoleManager<Role> _roleManager; 
        private readonly SignInManager<User> _signInManager; 
        private readonly AppDbContext _context; 
        

        public UserService(AppDbContext db,
                           IMapper mapper, 
                           ErrorHelper errorHelpers,
                           UserManager<User> userManager, 
                           RoleManager<Role> roleManager, 
                           SignInManager<User> signInManager, 
                           AppDbContext context
                           )
        {
            _db = db;
            _mapper = mapper;
            _errorHelpers = errorHelpers;
            _userManager = userManager; 
            _roleManager = roleManager; 
            _signInManager = signInManager; 
            _context = context; 
        }
        
        public async Task<ResponseDto<UserDto>> Register(UserRegisterDto model)
        {
            var response = new ResponseDto<UserDto>();

            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
                return await _errorHelpers.SetError(response, ErrorConstants.EmailInUse);

            var user = new User
            {
                UserName = model.Email,
                Email = model.Email,
            };

            var result = await _userManager.CreateAsync(user, model.Password);
            
            if (await _roleManager.RoleExistsAsync("User"))
            {
                await _userManager.AddToRoleAsync(user, "User");
            }

            response.Data = _mapper.Map<UserDto>(user);

            return response;
        }
        
        public async Task<bool> AuthenticateAsync(string username, string password) 
        { 
            var user = await _userManager.FindByNameAsync(username); 
            if (user == null) return false; 
 
            var result = await _signInManager.PasswordSignInAsync(user, password, false, false); 
            return result.Succeeded; 
        } 
 
        public async Task<List<User>> GetAllUsersAsync() 
        { 
            return await _userManager.Users 
                .Include(u => u.UserRoles) 
                .ThenInclude(ur => ur.Role) 
                .ToListAsync(); 
        }
        
        public async Task<User?> GetUserByIdAsync(long userId)
        {
            return await _db.Users.FindAsync(userId);
        }
    }
}
