using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ScientificReport.Models.ViewModels
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
	}
}
