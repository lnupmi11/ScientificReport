using System.Collections.Generic;

namespace ScientificReport.DAL.Entities.Reports
{
	public class FacultyReport : Report
	{
		public virtual UserProfile.UserProfile Administrator { get; set; }
		
		public virtual ICollection<DepartmentReport> DepartmentReports { get; set; }
	}
}
