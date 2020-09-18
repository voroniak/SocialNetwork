using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.DTOs
{
    public class PostDto
    {
        ObjectId Id { get; set; }

        DateTime CreatedAt { get; }
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}
