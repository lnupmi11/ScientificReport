using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class UserProfilesScientificInternships
	{
		[Key]
		public Guid Id { get; set; }
		
		public int UserProfileId { get; set; }
		public virtual UserProfile UserProfile { get; set; }
		
		public int ScientificInternshipId { get; set; }
		public virtual ScientificInternship ScientificInternship { get; set; }
	}
}
