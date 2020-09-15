using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Services.Implementation
{
    public class PostService
    {
        private readonly IMongoRepository<Post> _mongoRepository;

        public PostService(IMongoRepository<Post> mongoRepository)
        {
            _mongoRepository = mongoRepository;
        }
        public async Task AddPostAsync(Post post)
        {
            await _mongoRepository.InsertOneAsync(post);
        }
    }
}
