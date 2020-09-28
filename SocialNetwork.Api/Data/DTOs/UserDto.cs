using System.Collections.Generic;

namespace SocialNetwork.Api.Data.DTOs
{
    public class UserDto
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public IEnumerable<string> Interests { get; set; }
        public IEnumerable<string> Followers { get; set; }
        public string AplplicationUserId { get; set; }
    }
}
