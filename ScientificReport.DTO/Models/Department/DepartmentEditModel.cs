using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ScientificReport.DTO.Models.Department
{
	public class DepartmentEditModel
	{
		[Required]
		public string Title { get; set; }
		
		[Required]
		[Display(Name = "Head of Department")]
		public Guid SelectedHeadId { get; set; }
		
		public IEnumerable<SelectListItem> UserSelection { get; set; }
		
		public DAL.Entities.UserProfile.UserProfile Head { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
		
		public IEnumerable<DAL.Entities.ScientificWork> ScientificWorks { get; set; }
		
		public bool IsEditingByHead { get; set; }
	}
}
