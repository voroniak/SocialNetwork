using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Services.Implementation
{
    public class FollowerService
    {
        private readonly IMongoRepository<User> _mongoRepository;
        private readonly IMapper _mapper;

        public FollowerService(IMongoRepository<User> mongoRepository, IMapper mapper)
        {
            _mongoRepository = mongoRepository;
            _mapper = mapper;
        }

        public async Task FollowUserAsync(string followerUserId, string followingUserId)
        {
            var user = await _mongoRepository
                   .FindOneAsync(u => u.Id.ToString() == followerUserId);
            if (!user.Followers.Any(f => f == followingUserId))
            {
                user.Followers.Append(followingUserId);
                await _mongoRepository.ReplaceOneAsync(user);
            }

        }

        public async Task UnfollowUserAsync(string followerUserId, string followingUserId)
        {
            var user = await _mongoRepository
                   .FindOneAsync(u => u.Id.ToString() == followerUserId);
            if (user.Followers.Any(f => f == followingUserId))
            {
                user.Followers.ToList().Remove(followingUserId);
            }
            await _mongoRepository.ReplaceOneAsync(user);
        }


        public async Task<IEnumerable<UserDto>> GetAllUserFollowers(string userId)
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>
                (
                await _mongoRepository.FilterByAsync(u => u.Followers.Contains(userId))
                );
        }

        public async Task<IEnumerable<UserDto>> GetAllUserFollowing(string userId)
        {
            var user = await _mongoRepository
                   .FindOneAsync(u => u.Id.ToString() == userId);
            var users = new List<User>();
            foreach(var fId in user.Followers)
            {
                users.Add(await _mongoRepository.FindByIdAsync(fId));
            }
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }
    }
}
