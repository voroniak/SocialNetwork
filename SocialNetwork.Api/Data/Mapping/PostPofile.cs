using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;

namespace SocialNetwork.Api.Data.Mapping
{
    public class PostPofile: Profile
    {
        public PostPofile()
        {
            CreateMap<PostDto, Post>().ReverseMap();
        }
    }
}
