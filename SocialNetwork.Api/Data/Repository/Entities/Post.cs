using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Repository.Entities
{
    public class Post : Document
    {
        public string Text { get; set; }
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public ObjectId UserId { get; set; }


    }
}
