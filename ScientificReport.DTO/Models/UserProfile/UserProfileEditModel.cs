using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.UserProfile
{
	public class UserProfileEditModel
	{
		[Required]
		public string FirstName { get; set; }
		
		[Required]
		public string MiddleName { get; set; }
		
		[Required]
		public string LastName { get; set; }
		
		[Required]
		public string UserName { get; set; }
		
		[Required]
		public string Email { get; set; }
		
		public int BirthYear { get; set; }

		public int GraduationYear { get; set; }
		
		public string ScientificDegree { get; set; }
		
		public int YearDegreeGained { get; set; }
		
		public string AcademicStatus { get; set; }
		
		public int YearDegreeAssigned { get; set; }
		
		[Required]
		public string PhoneNumber { get; set; }
		
		public bool IsApproved { get; set; }
		
		public bool IsActive { get; set; }
		
		public Guid UserId { get; set; }
		
		public bool IsSelfEditing { get; set; }
		
		public IEnumerable<DAL.Roles.UserProfileRole> AllRoles { get; set; }
		
		public IEnumerable<string> UserRoles { get; set; }
		
		public bool IsHeadOfDepartment { get; set; }
	}
}
