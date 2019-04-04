using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesReviews
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile Reviewer { get; set; }
		
		[Required]
		public virtual Review Review { get; set; }
	}
}
