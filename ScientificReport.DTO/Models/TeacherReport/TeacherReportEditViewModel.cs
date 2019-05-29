using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.TeacherReport
{
	public class TeacherReportEditViewModel
	{
		public DAL.Entities.Reports.TeacherReport TeacherReport { get; set; }

		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
		public IEnumerable<DAL.Entities.Publication> Publications { get; set; }
	}
}