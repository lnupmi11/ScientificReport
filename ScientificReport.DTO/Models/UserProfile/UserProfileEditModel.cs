using System.Collections.Generic;

namespace ScientificReport.DTO.Models.UserProfile
{
	public class UserProfileEditModel
	{
		public DAL.Entities.UserProfile.UserProfile UserProfile { get; set; }
		
		public IEnumerable<DAL.Roles.UserProfileRole> AllRoles { get; set; }
		
		public IEnumerable<string> UserRoles { get; set; }
	}
}
