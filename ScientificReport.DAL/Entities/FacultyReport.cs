using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class FacultyReport : Report
	{
		public virtual UserProfile Administrator { get; set; }
		
		public virtual ICollection<DepartmentReport> DepartmentReports { get; set; }
	}
}
