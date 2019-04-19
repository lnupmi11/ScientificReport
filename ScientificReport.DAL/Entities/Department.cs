using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Department
	{
		[Key]
		public Guid Id { get; set; }
		
		public string Title { get; set; } 
 
		public virtual ICollection<ScientificWork> ScientificWorks { get; set; }
	}
}
