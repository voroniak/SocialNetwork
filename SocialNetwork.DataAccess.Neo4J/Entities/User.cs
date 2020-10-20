namespace SocialNetwork.DataAccess.Neo4J.Entities
{
    public class User : Neo4jEntity
    {
        public string UserId { get; set; }
        public User()
        {
            Label = "User";
        }
    }
}
