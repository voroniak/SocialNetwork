namespace SocialNetwork.Api.Data.DTOs
{
    public class CommentPostDto
    {
        public string UserId { get; set; }
        public string Text { get; set; }
        public string CommentedEntityId { get; set; }
    }
}
