using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class ApplicantsPatentLicenseActivities
	{
		[Key]
		public int Id { get; set; }
		
		public string ApplicantId { get; set; }
		public virtual UserProfile Applicant { get; set; }
		
		public int PatentLicenseActivityId { get; set; }
		public virtual PatentLicenseActivity PatentLicenseActivity { get; set; }
	}
}
