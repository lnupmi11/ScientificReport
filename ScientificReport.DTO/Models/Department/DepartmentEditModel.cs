using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Department
{
	
	public class DepartmentEditModel
	{
	//	public DAL.Entities.Department Department { get; set; }
		
		public Guid DepartmentId { get; set; }
	
		[Required]
		public string Title { get; set; }
	
		[Required]
		[Display(Name = "Head of Department")]
		public Guid SelectedHeadId { get; set; }
		
		public IEnumerable<SelectItem> UserSelection { get; set; }
		
		public IEnumerable<SelectItem> ScientificWorkItems { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Staff { get; set; }
		
		public DAL.Entities.UserProfile.UserProfile Head { get; set; }
		
		public IEnumerable<DAL.Entities.ScientificWork> ScientificWorks { get; set; }
		
		public bool IsEditingByHead { get; set; }
	}
}
