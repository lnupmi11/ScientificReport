using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesGrants
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> UserProfiles { get; set; }
		
		[Required]
		public virtual ICollection<Grant> Grants { get; set; }
	}
}
