using Newtonsoft.Json;

namespace SocialNetwork.DataAccess.Neo4J.Entities
{
    public class Neo4jRelationship
    {
        [JsonIgnore]
        public string Name { get; set; }
    }
}
