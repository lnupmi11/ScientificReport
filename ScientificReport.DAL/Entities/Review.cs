using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Review
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual Publication Work { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime DateOfReview { get; set; }
		
		public virtual UserProfile.UserProfile Reviewer { get; set; }
	}
}
