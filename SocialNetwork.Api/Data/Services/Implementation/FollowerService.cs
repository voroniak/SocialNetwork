using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;
using SocialNetwork.DataAccess.Neo4J.Entities;
using SocialNetwork.DataAccess.Neo4J.Repository;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Services.Implementation
{
    public class FollowerService
    {
        private readonly IMongoRepository<User> _mongoRepository;
        private readonly Neo4jRepository<Neo4JUser> _neo4JRepository;
        private readonly IMapper _mapper;

        public FollowerService(IMongoRepository<User> mongoRepository, IMapper mapper, Neo4jRepository<Neo4JUser> neo4JRepository)
        {
            _mongoRepository = mongoRepository;
            _mapper = mapper;
            _neo4JRepository = neo4JRepository;
        }

        public async Task FollowUserAsync(string followerUserId, string followingUserId)
        {
            var user = await _mongoRepository
                   .FindOneAsync(u => u.Id.ToString() == followerUserId);
            if (!user.Followers.Any(f => f == followingUserId))
            {
                user.Followers.Append(followingUserId);
                await _mongoRepository.ReplaceOneAsync(user);
                await _neo4JRepository.Relate<Neo4JUser, Neo4jRelationship>(
                    u1 => u1.UserId == followerUserId,
                    u2 => u2.UserId == followingUserId,
                    new Neo4jRelationship { Name = "Following" });
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
            await _neo4JRepository.DeleteRelationship<Neo4JUser, Neo4jRelationship>(
                    u1 => u1.UserId == followerUserId,
                    u2 => u2.UserId == followingUserId,
                    new Neo4jRelationship { Name = "Following" });
        }


        public async Task<IEnumerable<UserDto>> GetAllUserFollowersAsync(string userId)
        {
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>
                (
                await _mongoRepository.FilterByAsync(u => u.Followers.Contains(userId))
                );
        }

        public async Task<IEnumerable<UserDto>> GetAllUserFollowingAsync(string userId)
        {
            var user = await _mongoRepository
                   .FindOneAsync(u => u.Id.ToString() == userId);
            var users = new List<User>();
            foreach (var fId in user.Followers)
            {
                users.Add(await _mongoRepository.FindByIdAsync(fId));
            }
            return _mapper.Map<IEnumerable<User>, IEnumerable<UserDto>>(users);
        }

        public async Task<int> GetShortestPath(string followerUserId, string followingUserId)
        {
            return await _neo4JRepository.GetShortestPath<Neo4JUser, Neo4jRelationship>(
                  new Neo4JUser() { UserId = followerUserId },
                  new Neo4JUser() { UserId = followingUserId },
                  new Neo4jRelationship { Name = "Following" });
        }
    }
}
