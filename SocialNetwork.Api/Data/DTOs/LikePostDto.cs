using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Api.Data.DTOs
{
    public class LikePostDto
    {
        public string UserId { get; set; }
        public string LikedEntityId { get; set; }
    }
}
