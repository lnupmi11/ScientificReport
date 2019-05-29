using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class ApplicantsPatentLicenseActivities
	{
		[Key]
		public Guid Id { get; set; }
		
		public Guid ApplicantId { get; set; }
		public virtual UserProfile.UserProfile Applicant { get; set; }
		
		public Guid PatentLicenseActivityId { get; set; }
		public virtual PatentLicenseActivity PatentLicenseActivity { get; set; }
	}
}
