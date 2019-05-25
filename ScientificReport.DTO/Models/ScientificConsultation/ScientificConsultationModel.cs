using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.ScientificConsultation
{
	public class ScientificConsultationModel
	{
//		[Required]
		public Guid GuideId { get; set; }
		
		public DAL.Entities.UserProfile.UserProfile Guide { get; set; }

		[Required]
		public string CandidateName { get; set; }
		
		[Required]
		public string DissertationTitle { get; set; }
		
	    public ScientificConsultationModel()
    	{
    	}

        public ScientificConsultationModel(DAL.Entities.ScientificConsultation scientificConsultation)
        {
	        Guide = scientificConsultation.Guide;
	        CandidateName = scientificConsultation.CandidateName;
	        DissertationTitle = scientificConsultation.DissertationTitle;
        }
	}
}
