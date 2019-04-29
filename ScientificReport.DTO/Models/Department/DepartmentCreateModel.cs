using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScientificReport.DTO.Models.Department
{
	public class DepartmentCreateModel
	{
		[Required]
		public string Title { get; set; }
		
		[Required]
		[Display(Name = "Head of Department")]
		public Guid SelectedHeadId { get; set; }
		
		public IEnumerable<SelectListItem> UserSelection { get; set; }
	}
}
