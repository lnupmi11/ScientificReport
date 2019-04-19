using System.Collections.Generic;

namespace ScientificReport.DAL.Entities.Reports
{
	public class DepartmentReport : Report
	{
		public virtual UserProfile HeadOfDepartment { get; set; }
		
		public virtual ICollection<TeacherReport> TeacherReports { get; set; }
		
		public virtual ICollection<Conference> Conferences { get; set; }
	}
}
