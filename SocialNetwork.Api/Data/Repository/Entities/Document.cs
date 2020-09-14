using MongoDB.Bson;
using System;

namespace SocialNetwork.Api.Data.Repository.Entities
{
    public abstract class Document : IDocument
    {
        public ObjectId Id { get; set; }

        public DateTime CreatedAt => Id.CreationTime;
    }
}
