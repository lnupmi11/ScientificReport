using System;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DAL.Entities.Reports
{
	public class TeacherReportsPublications
	{
		[Key]
		public Guid Id { get; set; }
		
		public TeacherReport TeacherReport { get; set; }
		
		public Publication Publication { get; set; }

	}
}
