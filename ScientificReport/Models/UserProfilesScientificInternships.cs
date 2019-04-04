using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesScientificInternships
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile UserProfile { get; set; }
		
		[Required]
		public virtual ScientificInternship ScientificInternship { get; set; }
	}
}
