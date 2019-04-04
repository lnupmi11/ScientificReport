using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class PatentLicenseActivity
	{
		public enum Types
		{
			Patent, Application
		}
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string Name { get; set; }
		
		[Required]
		public int Number { get; set; }
		
		[Required, DataType(DataType.DateTime)]
		public DateTime DateTime { get; set; }
		
		[Required]
		public Types Type { get; set; }
		
		public virtual UserProfilesPatentLicenseActivities UserProfilesPatentLicenseActivities { get; set; }
	}
}
