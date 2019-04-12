using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class ScientificWork
	{
		[Key]
		public int Id { get; set; }
		
		public string Cypher { get; set; }
		
		public string Category { get; set; }
		
		public string Title { get; set; }
		
		public string Contents { get; set; }
		
		public virtual ICollection<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
	}
}
