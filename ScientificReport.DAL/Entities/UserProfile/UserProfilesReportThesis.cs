using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities.UserProfile
{
	public class UserProfilesReportThesis
	{
		[Key]
		public Guid Id { get; set; }		
		
		public virtual UserProfile UserProfile { get; set; }
		
		public virtual ReportThesis ReportThesis { get; set; }
	}
}
