using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ScientificReport.DTO.Models.UserProfile
{
	[SuppressMessage("ReSharper", "Mvc.TemplateNotResolved")]
	public class RegisterModel
	{
		[Required]
		public string UserName { get; set; }
		
		[Required]
		[UIHint("email")]
		public string Email { get; set; }
		
		[Required]
		[UIHint("password")]
		public string Password { get; set; }
		
		[Required]
		[UIHint("password")]
		public string PasswordRepeat { get; set; }

		[Required]
		public string FirstName { get; set; }
		
		[Required]
		public string MiddleName { get; set; }
		
		[Required]
		public string LastName { get; set; }
		
		[Required]
		public string PhoneNumber { get; set; }
		
		[Required]
		[Display(Name = "ExpectedDepartment")]
		public Guid SelectedDepartmentId { get; set; }
		
		public IEnumerable<DAL.Entities.Department> Departments { get; set; }
	}
}
