using MongoDB.Bson;

namespace SocialNetwork.Api.Data.DTOs
{
    public class LikeGetDto
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string LikedEntityId { get; set; }
    }
}
