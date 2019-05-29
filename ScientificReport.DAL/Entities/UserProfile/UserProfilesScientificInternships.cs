using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities.UserProfile
{
	public class UserProfilesScientificInternships
	{
		[Key]
		public Guid Id { get; set; }
		
		public Guid UserProfileId { get; set; }
		public virtual UserProfile UserProfile { get; set; }
		
		public Guid ScientificInternshipId { get; set; }
		public virtual ScientificInternship ScientificInternship { get; set; }
	}
}
