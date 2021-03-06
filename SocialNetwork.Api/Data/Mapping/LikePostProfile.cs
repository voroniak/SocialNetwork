﻿using AutoMapper;
using SocialNetwork.Api.Data.DTOs;
using SocialNetwork.Api.Data.Repository.Entities;

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
