using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using NPUApp.Database.Context;
using NPUApp.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NPUApp.BLL.Services
{
    public class UserService
    {
        private NpuAppDbContext _context { get; set; }
        private IMapper _mapper { get; set; }
        private UserManager<User> _userManager { get; set; }
        private TokenService _tokenService { get; set; }
        private IHttpContextAccessor _httpContextAccessor { get; set; }
        public UserService(NpuAppDbContext context, UserManager<User> userManager, TokenService tokenService, IHttpContextAccessor contextAccessor, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
            _tokenService = tokenService;
            _httpContextAccessor = contextAccessor;
        }

        public async Task<IdentityResult?> Register(string username, string email, string password)
        {
            var result = await _userManager.CreateAsync(
            new User { UserName = username, Email = email },
                password
            );
            return result;
        }

        public async Task<bool> AreCredentialsValid(string email, string password)
        {
            var managedUser = await _userManager.FindByEmailAsync(email);
            if (managedUser == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(managedUser, password);
        }

        public async Task<string> Authorize(string email, string password)
        {
            var areCredentialsValid = await AreCredentialsValid(email, password);

            if (!areCredentialsValid)
            {
                throw new ArgumentException("Bad credentials", "Authorization");
            }

            var userInDb = _context.Users.FirstOrDefault(u => u.Email == email);
            if (userInDb is null)
                throw new InvalidOperationException("User validation error");
            var accessToken = _tokenService.CreateToken(userInDb);
            await _context.SaveChangesAsync();
            return accessToken;
        }

        public async Task<User?> GetAuthorizedUser()
        {
            var claims = _httpContextAccessor.HttpContext?.User;
            if (claims == null) return null;
            return await _userManager.GetUserAsync(claims) ?? null;
        }
    }
}
