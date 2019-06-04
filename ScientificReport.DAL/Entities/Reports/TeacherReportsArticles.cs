using System;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DAL.Entities.Reports
{
	public class TeacherReportsArticles
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual TeacherReport TeacherReport { get; set; }
		
		public virtual Article Article { get; set; }

	}
}
