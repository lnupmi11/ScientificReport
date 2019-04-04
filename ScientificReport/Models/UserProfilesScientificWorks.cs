using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesScientificWorks
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> UserProfile { get; set; }
		
		[Required]
		public virtual ICollection<ScientificWork> ScientificWork { get; set; }
	}
}
