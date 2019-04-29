using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class ScientificWork
	{
		[Key]
		public Guid Id { get; set; }
		
		public string Cypher { get; set; }
		
		public string Category { get; set; }
		
		public string Title { get; set; }
		
		public string Contents { get; set; }
		
		public ICollection<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
	}
}
