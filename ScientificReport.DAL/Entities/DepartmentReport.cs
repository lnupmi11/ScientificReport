using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class DepartmentReport : Report
	{
		public virtual UserProfile HeadOfDepartment { get; set; }
		
		public virtual ICollection<TeacherReport> TeacherReports { get; set; }
		
		public virtual ICollection<Conference> Conferences { get; set; }
	}
}
