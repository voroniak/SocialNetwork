using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;
using SocialNetwork.Api.Data.Repository.Repo;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        public async Task AddAsync(CommentPostDto commentPostDto)
        {
            await _mongoRepository.InsertOneAsync(_mapper.Map<CommentPostDto, Comment>(commentPostDto));
        }

        public async Task DeleteAsync(string commentId)
        {
            await _mongoRepository.DeleteByIdAsync(commentId);
        }

        public async Task<IEnumerable<CommentGetDto>> GetAllAsync()
        {
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentGetDto>>(await _mongoRepository.FilterByAsync(_ => true));
        }

        public async Task<IEnumerable<CommentGetDto>> GetByCommentedEnityIdAsync(string commentedEntityId)
        {
            return _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentGetDto>>
                (
                await _mongoRepository.FilterByAsync(c => c.CommentedEntityId == commentedEntityId)
                );
        }
        public async Task EditAsync(CommentPutDto commentPutDto)
        {
            var commentToEdit = await _mongoRepository.FindByIdAsync(commentPutDto.Id);
            if(commentToEdit == null)
            {
                throw new ArgumentNullException($"Comment with id {commentPutDto.Id} does not exist");
            }
            commentToEdit.Text = commentPutDto.Text;

            await _mongoRepository.ReplaceOneAsync(commentToEdit);
        }
    }
}
