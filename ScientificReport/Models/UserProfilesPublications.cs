using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesPublications
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile UserProfile { get; set; }
		
		[Required]
		public virtual Publication Publication { get; set; }
	}
}
