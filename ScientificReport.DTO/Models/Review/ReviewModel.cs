using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Review
{
	public class ReviewModel
	{
		[Required]
		public Guid WorkId { get; set; }
		
		public DAL.Entities.Publications.Publication Work { get; set; }
		
		public IEnumerable<DAL.Entities.Publications.Publication> Publications { get; set; }
		
		public DAL.Entities.UserProfile.UserProfile Reviewer { get; set; }
        
		[Required]
        [DataType(DataType.Date)]
        public DateTime DateOfReview { get; set; }
		
	    public ReviewModel()
    	{
    	}

        public ReviewModel(DAL.Entities.Review review)
        {
	        Reviewer = review.Reviewer;
	        Work = review.Work;
	        DateOfReview = review.DateOfReview;
        }
	}
}
