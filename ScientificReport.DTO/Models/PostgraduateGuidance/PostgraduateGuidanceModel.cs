using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.PostgraduateGuidance
{
	public class PostgraduateGuidanceModel
	{
//		[Required]
		public Guid GuideId { get; set; }

		public DAL.Entities.UserProfile.UserProfile Guide { get; set; }

		[Required]
		public string PostgraduateName { get; set; }
		
		[Required]
		public string PostgraduateInfo { get; set; }
		
	    public PostgraduateGuidanceModel()
    	{
    	}

        public PostgraduateGuidanceModel(DAL.Entities.PostgraduateGuidance postgraduateGuidance)
        {
	        Guide = postgraduateGuidance.Guide;
	        PostgraduateName = postgraduateGuidance.PostgraduateName;
	        PostgraduateInfo = postgraduateGuidance.PostgraduateInfo;
        }
	}
}
