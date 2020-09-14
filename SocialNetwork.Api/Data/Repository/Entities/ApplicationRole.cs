using AspNetCore.Identity.MongoDbCore.Models;
using System;

namespace SocialNetwork.Api.Data.Repository.Entities
{
    public class ApplicationRole: MongoIdentityRole<Guid>
    {
		public ApplicationRole() : base()
		{
		}

		public ApplicationRole(string roleName) : base(roleName)
		{
		}
	}
}
