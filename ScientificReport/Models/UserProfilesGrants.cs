using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesGrants
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile UserProfile { get; set; }
		
		[Required]
		public virtual Grant Grant { get; set; }
	}
}
