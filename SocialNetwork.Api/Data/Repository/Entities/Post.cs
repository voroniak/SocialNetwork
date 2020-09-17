using System.Collections.Generic;

namespace SocialNetwork.Api.Data.Repository.Entities
{
    [BsonCollection("Posts")]
    public class Post : Document
    {
        public string Text { get; set; }

        public string UserId { get; set; }
    }
}
