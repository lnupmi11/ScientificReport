using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class AuthorsPatentLicenseActivities
	{
		[Key]
		public Guid Id { get; set; }
		
		public int AuthorId { get; set; }
		public virtual UserProfile Author { get; set; }
		
		public int PatentLicenseActivityId { get; set; }
		public virtual PatentLicenseActivity PatentLicenseActivity { get; set; }
	}
}
