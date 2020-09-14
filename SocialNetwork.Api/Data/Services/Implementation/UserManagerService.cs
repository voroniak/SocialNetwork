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

        public UserManagerService(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager; 
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
    }
}
