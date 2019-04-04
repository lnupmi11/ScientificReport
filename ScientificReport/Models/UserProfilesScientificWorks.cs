using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesScientificWorks
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile UserProfile { get; set; }
		
		[Required]
		public virtual ScientificWork ScientificWork { get; set; }
	}
}
