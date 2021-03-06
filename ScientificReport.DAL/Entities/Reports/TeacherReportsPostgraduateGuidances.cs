using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities.Reports
{
	public class TeacherReportsPostgraduateGuidances
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual TeacherReport TeacherReport { get; set; }
		
		public virtual PostgraduateGuidance PostgraduateGuidance { get; set; }

	}
}
