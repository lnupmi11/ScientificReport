using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ScientificReport.Models
{
	public enum UserType
	{
		Teacher, HeadOfDepartment, Admin,
	}

	public class UserProfile : IdentityUser<int>
	{
		public UserType Type { get; set; }

		[Required]
		public string FirstName { get; set; }
		
		[Required]
		public string MiddleName { get; set; }
		
		[Required]
		public string LastName { get; set; }
		
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
		public string Position { get; set; }
		
		[Required]
		public bool IsApproved { get; set; }
	}
}
