using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities;

namespace ScientificReport.DTO.Models.Department
{
	
	public class DepartmentEditModel
	{
		public DAL.Entities.Department Department { get; set; }
		
		public Guid DepartmentId { get; set; }
	
		[Required]
		public string Title { get; set; }
	
		[Required]
		[Display(Name = "Head of Department")]
		public Guid SelectedHeadId { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> UserSelection { get; set; }
		
		public IEnumerable<ScientificWork> AvailableScientificWork { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Staff { get; set; }
		
		public DAL.Entities.UserProfile.UserProfile Head { get; set; }
		
		public IEnumerable<ScientificWork> ScientificWorks { get; set; }
		
		public bool IsEditingByHead { get; set; }
	}
}
