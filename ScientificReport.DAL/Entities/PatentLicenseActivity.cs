using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class PatentLicenseActivity
	{
		public enum Types
		{
			Patent, Application
		}
		
		[Key]
		public Guid Id { get; set; }
		
		public string Name { get; set; }
		
		public int Number { get; set; }
		
		[DataType(DataType.DateTime)]
		public DateTime Date { get; set; }
		
		public Types Type { get; set; }
		
		public virtual ICollection<AuthorsPatentLicenseActivities> AuthorsPatentLicenseActivities { get; set; }
		
		public virtual ICollection<CoauthorsPatentLicenseActivities> CoauthorsPatentLicenseActivities { get; set; }
		
		public virtual ICollection<ApplicantsPatentLicenseActivities> ApplicantsPatentLicenseActivities { get; set; }
		
		public virtual ICollection<CoApplicantsPatentLicenseActivities> CoApplicantsPatentLicenseActivities { get; set; }
	}
}
