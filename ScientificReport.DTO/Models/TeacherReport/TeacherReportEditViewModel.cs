using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.TeacherReport
{
	public class TeacherReportEditViewModel
	{
		public DAL.Entities.Reports.TeacherReport TeacherReport { get; set; }

		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
		public IEnumerable<DAL.Entities.Publication> Publications { get; set; }
		public IEnumerable<DAL.Entities.Article> Articles { get; set; }
		public IEnumerable<DAL.Entities.ScientificWork> ScientificWorks { get; set; }
		public IEnumerable<DAL.Entities.ReportThesis> ReportTheses { get; set; }
	}
}