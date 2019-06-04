using System;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DAL.Entities.UserProfile
{
	public class UserProfilesScientificWorks
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual UserProfile UserProfile { get; set; }
		
		public virtual ScientificWork ScientificWork { get; set; }
	}
}
