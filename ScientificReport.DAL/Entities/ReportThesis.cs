using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities
{
	public class ReportThesis
	{
		[Key]
		public Guid Id { get; set; }
		
		public string Thesis { get; set; }
		
		public virtual Conference Conference { get; set; }
		
		public virtual ICollection<UserProfilesReportThesis> UserProfilesReportTheses { get; set; }
	}
}
