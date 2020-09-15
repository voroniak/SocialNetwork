using Microsoft.AspNetCore.Identity;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Services.Implementation
{
    public class UserManagerService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public UserManagerService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<string> GetUserIdAsync(ClaimsPrincipal user)
        {
            var userAsync = await _userManager.GetUserAsync(user);
            if (userAsync == null)
            {
                // Return null if User not authorized.
                return null;
            }
            return _userManager.GetUserId(user);
        }
        public async Task<IdentityResult> CreateUserAsync(RegisterDto registerDto)
        {
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                LastName = registerDto.LastName,
                FirstName = registerDto.FirstName,
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            return result;
        }
        public async Task<SignInResult> SignInAsync(LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.RememberMe, true);
            return result;
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
        public async Task<ApplicationUser> FindByEmailAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user;
        }

    }
}
