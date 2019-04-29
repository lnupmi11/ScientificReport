using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.UserProfile
{
	public class UserProfileEditModel
	{
		public DAL.Entities.UserProfile UserProfile { get; set; }
		
		public IEnumerable<DAL.Roles.UserProfileRole> Roles { get; set; }
	}
}
