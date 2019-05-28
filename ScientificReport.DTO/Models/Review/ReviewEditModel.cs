using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Review
{
	public class ReviewEditModel : ReviewModel
	{
	    [Required]
		public Guid Id { get; set; }

		public ReviewEditModel()
        {
        }

        public ReviewEditModel(DAL.Entities.Review review) : base(review)
        {
        	Id = review.Id;
        }
	}
}
