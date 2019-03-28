using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Paper
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public virtual ICollection<UserProfile> Authors { get; set; }
	}
}
