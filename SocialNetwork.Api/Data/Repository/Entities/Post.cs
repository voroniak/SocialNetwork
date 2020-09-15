using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Repository.Entities
{
    [CollectionName("Posts")]
    public class Post : Document
    {
        public string Text { get; set; }
        
        public ObjectId UserId { get; set; }

        public  IEquatable<ObjectId> Likes { get; set; }

    }
}
