using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities
{
	public class Review
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual Publication Work { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime DateOfReview { get; set; }
		
		public virtual ICollection<UserProfilesReviews> UserProfilesReviews { get; set; }
	}
}
