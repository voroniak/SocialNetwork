using AutoMapper;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;

namespace SocialNetwork.Api.Data.Services.Implementation
{
    public class CommentService
    {
        private readonly IMongoRepository<Comment> _mongoRepository;
        private readonly IMapper _mapper;

        public CommentService(IMongoRepository<Comment> mongoRepository, IMapper mapper)
        {
            _mongoRepository = mongoRepository;
            _mapper = mapper;
        }
    }
}
