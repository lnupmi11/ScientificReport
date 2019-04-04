using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesArticles
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Authors { get; set; }
		
		[Required]
		public virtual ICollection<Article> Articles { get; set; }
	}
}
