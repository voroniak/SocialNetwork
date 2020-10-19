using Newtonsoft.Json;
using SocialNetwork.DataAccess.Neo4J.Interfaces;
using System;
using System.Xml.Serialization;

namespace SocialNetwork.DataAccess.Neo4J.Entities
{
    public class Neo4jEntity : IEntity
    {
        protected Neo4jEntity()
        {
            LastUpdated = DateTime.UtcNow;
        }

        [JsonIgnore]
        [XmlIgnore]
        public string Label { get; protected set; }

        [JsonIgnore]
        [XmlIgnore]
        public DateTimeOffset LastUpdated { get; set; }
    }
}
