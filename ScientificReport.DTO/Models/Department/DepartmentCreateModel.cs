using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Department
{
	public class DepartmentCreateModel
	{
		[Required]
		public string Title { get; set; }
		
		[Required]
		[Display(Name = "Head of Department")]
		public Guid SelectedHeadId { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> UserSelection { get; set; }
	}
}
