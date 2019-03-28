using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Department
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string Title { get; set; } 
 
		public virtual ICollection<ScientificWork> ScientificWorks { get; set; }
	}
}
