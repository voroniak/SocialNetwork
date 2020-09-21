using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;

namespace SocialNetwork.Api.Data.Mapping
{
    public class LikeGetProfile: Profile
    {
        public LikeGetProfile()
        {
            CreateMap<LikeGetDto, Like>().ReverseMap();
        }
    }
}
