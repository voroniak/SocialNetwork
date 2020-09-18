using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Mapping
{
    public class LikePostProfile: Profile
    {
        public LikePostProfile()
        {
            CreateMap<Like, LikePostDto>().ReverseMap();
        }
    }
}
