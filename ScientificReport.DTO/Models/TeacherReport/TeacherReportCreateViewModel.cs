using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.TeacherReport
{
	public class TeacherReportCreateViewModel
	{
		[Required]
		public string UserId;
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users;
	}
}
