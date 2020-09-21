using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Services.Implementation
{
    public class LikeService
    {
        private readonly IMongoRepository<Like> _mongoRepository;
        private readonly IMapper _mapper;

        public LikeService(IMongoRepository<Like> mongoRepository, IMapper mapper)
        {
            _mongoRepository = mongoRepository;
            _mapper = mapper;
        }

        public async Task AddAsync(LikePostDto likePostDto)
        {
            await _mongoRepository.InsertOneAsync(_mapper.Map<LikePostDto, Like>(likePostDto));
        }

        public async Task DeleteAsync(string likeId)
        {
            await _mongoRepository.DeleteByIdAsync(likeId);
        }

    }
}
