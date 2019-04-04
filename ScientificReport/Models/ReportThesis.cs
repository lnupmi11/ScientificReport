using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class ReportThesis
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string Thesis { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Authors { get; set; }
		
		[Required]
		public virtual Conference Conference { get; set; }
	}
}
