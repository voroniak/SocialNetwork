namespace SocialNetwork.DataAccess.Neo4J.Entities
{
    public class Neo4JUser : Neo4jEntity
    {
        public string UserId { get; set; }
        public Neo4JUser()
        {
            Label = "User";
        }
    }
}
