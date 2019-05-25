using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.PostgraduateDissertationGuidance
{
	public class PostgraduateDissertationGuidanceEditModel : PostgraduateDissertationGuidanceModel
	{
	    [Required]
		public Guid Id { get; set; }

		public PostgraduateDissertationGuidanceEditModel()
        {
        }

        public PostgraduateDissertationGuidanceEditModel(DAL.Entities.PostgraduateDissertationGuidance postgraduateDissertationGuidance) : base(postgraduateDissertationGuidance)
        {
        	Id = postgraduateDissertationGuidance.Id;
        }
	}
}
