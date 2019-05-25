using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Review
{
	public class ReviewModel
	{
//		[Required]
		public Guid WorkId { get; set; }
		
		public DAL.Entities.Publication Work { get; set; }
        
		[Required]
        [DataType(DataType.Date)]
        public DateTime DateOfReview { get; set; }
		
	    public ReviewModel()
    	{
    	}

        public ReviewModel(DAL.Entities.Review review)
        {
	        Work = review.Work;
	        DateOfReview = review.DateOfReview;
        }
	}
}
