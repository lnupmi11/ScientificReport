using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class ScientificWork
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string Cypher { get; set; }
		
		[Required]
		public string Category { get; set; }
		
		[Required]
		public string Title { get; set; }
		
		[Required]
		public string Contents { get; set; }
		
		[Required]
		public virtual ICollection<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
	}
}
