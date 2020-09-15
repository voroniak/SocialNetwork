using AspNetCore.Identity.MongoDbCore.Models;
using MongoDB.Bson;
using MongoDbGenericRepository.Attributes;
using System;

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
		public IEquatable<string> Interests { get; set; }
		public IEquatable<ObjectId> Posts { get; set; }
	}
}
