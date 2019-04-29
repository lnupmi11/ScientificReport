using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities
{
	public class ScientificInternship
	{
		[Key]
		public Guid Id { get; set; }

		public string PlaceOfInternship { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime Started { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime Ended { get; set; }

		public string Contents { get; set; }
		
		public virtual ICollection<UserProfilesScientificInternships> UserProfilesScientificInternships { get; set; }
	}
}
