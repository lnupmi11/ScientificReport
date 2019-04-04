using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesPublications
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> UserProfiles { get; set; }
		
		[Required]
		public virtual ICollection<Publication> Publications { get; set; }
	}
}
