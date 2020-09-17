namespace SocialNetwork.Api.Data.Repository.Entities
{
    [BsonCollection("Likes")]
    public class Like : Document
    {
        public string UserId { get; set; }
        public string LikedEntityId { get; set; }

    }
}
