using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesPatentLicenseActivities
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Authors { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Applicants { get; set; }
		
		[Required]
		public virtual ICollection<PatentLicenseActivity> PatentLicenseActivities { get; set; }
	}
}
