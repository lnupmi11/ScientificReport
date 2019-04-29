using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class ScientificConsultation
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual UserProfile.UserProfile Guide { get; set; }

		public string Name { get; set; }
		
		public string DissertationTitle { get; set; }
	}
}
