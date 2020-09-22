using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;

namespace SocialNetwork.Api.Data.Mapping
{
    public class CommentPostProfile:Profile
    {
        public CommentPostProfile()
        {
            CreateMap<CommentPostDto, Comment>().ReverseMap();
        }
    }
}
