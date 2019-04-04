using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Grant
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfilesGrants> UserProfilesGrants { get; set; }
	}
}
