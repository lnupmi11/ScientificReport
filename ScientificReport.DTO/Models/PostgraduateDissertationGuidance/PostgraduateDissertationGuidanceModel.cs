using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.PostgraduateDissertationGuidance
{
	public class PostgraduateDissertationGuidanceModel
	{
		[Required]
		public DAL.Entities.UserProfile.UserProfile Guide { get; set; }
		
		[Required]
		public string PostgraduateName { get; set; }
		
		[Required]
		public string Dissertation { get; set; }
		
		[Required]
		public string Speciality { get; set; }
		
		[Required]
		[DataType(DataType.Date)]
		public DateTime DateDegreeGained { get; set; }
		
		[Required]
		public int GraduationYear { get; set; }
		
	    public PostgraduateDissertationGuidanceModel()
    	{
    	}

        public PostgraduateDissertationGuidanceModel(DAL.Entities.PostgraduateDissertationGuidance postgraduateDissertationGuidance)
        {
	        Guide = postgraduateDissertationGuidance.Guide;
	        PostgraduateName = postgraduateDissertationGuidance.PostgraduateName;
	        Dissertation = postgraduateDissertationGuidance.Dissertation;
	        Speciality = postgraduateDissertationGuidance.Speciality;
	        DateDegreeGained = postgraduateDissertationGuidance.DateDegreeGained;
	        GraduationYear = postgraduateDissertationGuidance.GraduationYear;
        }
	}
}
