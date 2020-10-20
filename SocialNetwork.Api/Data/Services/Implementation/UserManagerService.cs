using Microsoft.AspNetCore.Identity;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;
using SocialNetwork.DataAccess.Neo4J.Repository;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Services.Implementation
{
    public class UserManagerService
    {
        private UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private IMongoRepository<User> _mongoRepository;
        private Neo4jRepository<DataAccess.Neo4J.Entities.User> _neo4JRepository;
        public UserManagerService(UserManager<ApplicationUser> userManager,
                                  SignInManager<ApplicationUser> signInManager,
                                  IMongoRepository<User> mongoRepository,
                                  Neo4jRepository<DataAccess.Neo4J.Entities.User> neo4JRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mongoRepository = mongoRepository;
            _neo4JRepository = neo4JRepository;
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
            await _mongoRepository.InsertOneAsync(new User {
                                                            FirstName = registerDto.FirstName,
                                                            LastName = registerDto.LastName
                                                            });
          var regUser=(await _mongoRepository.FilterByAsync(u => u.FirstName == registerDto.FirstName && u.LastName == registerDto.LastName))
                .LastOrDefault();
            var id = regUser.Id.ToString();
            var user = new ApplicationUser()
            {
                Email = registerDto.Email,
                UserName = registerDto.Email,
                LastName = registerDto.LastName,
                FirstName = registerDto.FirstName,
                UserId = regUser.Id.ToString()
            };
           

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            var createdUser = await _userManager.FindByEmailAsync(registerDto.Email);
            var createdId = createdUser.Id.ToString();
            regUser.AplplicationUserId = createdId;
            await _mongoRepository.ReplaceOneAsync(regUser);
            await _neo4JRepository.Add(new DataAccess.Neo4J.Entities.User() { UserId = regUser.AplplicationUserId });
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

        public async Task<User> GetUserByIdAsync(string userId)
        {
         return  await _mongoRepository.FindOneAsync(u => u.AplplicationUserId == userId);
        }
       
    }
}
