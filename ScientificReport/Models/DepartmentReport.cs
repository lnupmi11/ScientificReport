using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class DepartmentReport : Report
	{
		[Required]
		public virtual UserProfile HeadOfDepartment { get; set; }
		
		public virtual ICollection<TeacherReport> TeacherReports { get; set; }
		
		public virtual ICollection<Conference> Conferences { get; set; }
	}
}
