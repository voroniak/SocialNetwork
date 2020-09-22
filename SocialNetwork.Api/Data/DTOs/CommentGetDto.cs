using MongoDB.Bson;

namespace SocialNetwork.Api.Data.DTOs
{
    public class CommentGetDto
    {
        public ObjectId Id { get; set; }
        public string UserId { get; set; }
        public string Text { get; set; }
        public string CommentedEntity { get; set; }
    }
}
