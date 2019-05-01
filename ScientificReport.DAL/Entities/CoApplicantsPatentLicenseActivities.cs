using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class CoApplicantsPatentLicenseActivities
	{
		
		[Key] 
		public Guid Id { get; set; }

		public string CoApplicant { get; set; }

		public int PatentLicenseActivityId { get; set; }
		
		public virtual PatentLicenseActivity PatentLicenseActivity { get; set; }
	}
}
