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
		
		[Required]
		public int BirthYear { get; set; }

		[Required]
		public int GraduationYear { get; set; }
		
		[Required]
		public string ScientificDegree { get; set; }
		
		[Required]
		public int YearDegreeGained { get; set; }
		
		[Required]
		public string AcademicStatus { get; set; }
		
		[Required]
		public int YearDegreeAssigned { get; set; }
		
		[Required]
		public string PhoneNumber { get; set; }
		
		[Required]
		public bool IsApproved { get; set; }
		
		[Required]
		public bool IsActive { get; set; }
		
		public Guid UserId { get; set; }
		
		public bool IsSelfEditing { get; set; }
		
		public IEnumerable<DAL.Roles.UserProfileRole> AllRoles { get; set; }
		
		public IEnumerable<string> UserRoles { get; set; }
	}
}
