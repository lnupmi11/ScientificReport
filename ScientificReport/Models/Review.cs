using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Review
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual Publication Work { get; set; }
		
		[Required, DataType(DataType.Date)]
		public DateTime DateOfReview { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Reviewers { get; set; }
	}
}
