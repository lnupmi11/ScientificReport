using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class UserProfilesReportThesis
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile UserProfile { get; set; }
		
		[Required]
		public virtual ReportThesis ReportThesis { get; set; }
	}
}
