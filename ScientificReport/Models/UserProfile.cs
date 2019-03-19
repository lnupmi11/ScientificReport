using Microsoft.AspNetCore.Identity;

namespace ScientificReport.Models
{
	public enum UserType
	{
		Admin
	}

	public class UserProfile
	{
		public int Id { get; set; }
		public UserType Type { get; set; }

		public IdentityUser User { get; set; }

		public string FirstName { get; set; }
		public string MiddleName { get; set; }
		public string LastName { get; set; }
	}
}