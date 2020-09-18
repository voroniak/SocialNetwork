using MongoDB.Bson;
using System;


namespace SocialNetwork.Api.Data.DTOs
{
    public class PostDto
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt { get; }
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}
