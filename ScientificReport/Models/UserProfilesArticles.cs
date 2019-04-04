using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesArticles
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile Author { get; set; }
		
		[Required]
		public virtual Article Article { get; set; }
	}
}
