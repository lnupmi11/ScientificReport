using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesPatentLicenseActivities
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile Author { get; set; }
		
		[Required]
		public virtual UserProfile Applicant { get; set; }
		
		[Required]
		public virtual PatentLicenseActivity PatentLicenseActivity { get; set; }
	}
}
