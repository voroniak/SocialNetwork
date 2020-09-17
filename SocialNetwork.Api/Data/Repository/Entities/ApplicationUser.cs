using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using System;
using System.Collections.Generic;

namespace SocialNetwork.Api.Data.Repository.Entities
{
	[CollectionName("Users")]
	public class ApplicationUser: MongoIdentityUser<Guid>
    {
		public ApplicationUser() : base()
		{
		}

		public ApplicationUser(string userName, string email) : base(userName, email)
		{
		}
        public string FirstName { get; set; }
		public string LastName { get; set; }
		public IEnumerable<string> Interests { get; set; }
		public IEnumerable<ObjectId> Followers { get; set; }

	}
}
