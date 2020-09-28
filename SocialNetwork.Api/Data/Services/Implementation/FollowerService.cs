using AutoMapper;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;

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
    }
}
