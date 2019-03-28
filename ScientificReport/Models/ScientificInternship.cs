using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class ScientificInternship
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Users { get; set; }
	}
}
