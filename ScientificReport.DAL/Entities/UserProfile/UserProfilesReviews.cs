using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class UserProfilesReviews
	{
		[Key]
		public int Id { get; set; }
		
		public int ReviewerId { get; set; }
		public virtual UserProfile Reviewer { get; set; }
		
		public int ReviewId { get; set; }
		public virtual Review Review { get; set; }
	}
}
