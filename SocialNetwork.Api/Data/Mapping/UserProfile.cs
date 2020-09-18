using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;

namespace SocialNetwork.Api.Data.Mapping
{
    public class UserProfile: Profile
    {
        public UserProfile()
        {
            CreateMap<ApplicationUser, UserDto>().ReverseMap();
        }
    }
}
