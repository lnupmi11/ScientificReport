using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesReviews
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Reviewers { get; set; }
		
		[Required]
		public virtual ICollection<Review> Reviews { get; set; }
	}
}
