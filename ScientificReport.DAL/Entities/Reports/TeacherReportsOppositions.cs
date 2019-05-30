using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities.Reports
{
	public class TeacherReportsOppositions
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual TeacherReport TeacherReport { get; set; }
		
		public virtual Opposition Opposition { get; set; }

	}
}
