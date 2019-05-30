using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class AuthorsPatentLicenseActivities
	{
		[Key]
		public Guid Id { get; set; }
		
		public Guid AuthorId { get; set; }
		public virtual UserProfile.UserProfile Author { get; set; }
		
		public Guid PatentLicenseActivityId { get; set; }
		
		public virtual PatentLicenseActivity PatentLicenseActivity { get; set; }
	}
}
