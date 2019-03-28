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
		public string Theme { get; set; }
		
		[Required]
		public string Content { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Participators { get; set; }
	}
}
