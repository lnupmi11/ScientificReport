using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.PostgraduateGuidance
{
	public class PostgraduateGuidanceEditModel : PostgraduateGuidanceModel
	{
	    [Required]
		public Guid Id { get; set; }

		public PostgraduateGuidanceEditModel()
        {
        }

        public PostgraduateGuidanceEditModel(DAL.Entities.PostgraduateGuidance postgraduateGuidance) : base(postgraduateGuidance)
        {
        	Id = postgraduateGuidance.Id;
        }
	}
}
