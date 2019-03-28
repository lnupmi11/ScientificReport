using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class FacultyReport : Report
	{
		[Required]
		public virtual UserProfile Administrator { get; set; }
		
		public virtual ICollection<DepartmentReport> DepartmentReports { get; set; }
	}
}
