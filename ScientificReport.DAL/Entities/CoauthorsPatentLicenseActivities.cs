using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class CoauthorsPatentLicenseActivities
	{
		[Key] 
		public Guid Id { get; set; }

		public string Coauthor { get; set; }

		public int PatentLicenseActivityId { get; set; }
		public virtual PatentLicenseActivity PatentLicenseActivity { get; set; }
	}
}