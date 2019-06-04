using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.TeacherReport
{
	public class TeacherReportCreateViewModel
	{
		[Required]
		public Guid UserId;
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }

		
	}
}
