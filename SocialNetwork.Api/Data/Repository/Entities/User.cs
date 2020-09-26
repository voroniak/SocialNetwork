using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.Repository.Entities
{
    [BsonCollection("Users")]
    public class User : Document
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Interests { get; set; }
        public IEnumerable<string> Followers { get; set; }
        public string AplplicationUserId { get; set; }
    }
}
