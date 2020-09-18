using AutoMapper;
using MongoDB.Driver;
using SocialNetwork.Api.Data.DTOs;
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
        private readonly IMapper _mapper;

        public PostService(IMongoRepository<Post> mongoRepository, IMapper mapper)
        {
            _mongoRepository = mongoRepository;
            _mapper = mapper;
        }
        public async Task AddPostAsync(PostDto post)
        {
            await _mongoRepository.InsertOneAsync(_mapper.Map<PostDto,Post>(post));
        }
        public async Task<IEnumerable<PostDto>> GetAll()
        {
         return _mapper.Map<IEnumerable<Post>,IEnumerable <PostDto >>(await _mongoRepository.FilterByAsync(_ => true));
        }

    }
}
