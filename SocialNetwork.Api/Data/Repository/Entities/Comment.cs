
namespace SocialNetwork.Api.Data.Repository.Entities
{
    public class Comment: Document
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string CommentedEntity { get; set; }
    }
}
