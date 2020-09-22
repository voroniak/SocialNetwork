
namespace SocialNetwork.Api.Data.Repository.Entities
{
    [BsonCollection("Likes")]
    public class Comment: Document
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string CommentedEntityId { get; set; }
    }
}
