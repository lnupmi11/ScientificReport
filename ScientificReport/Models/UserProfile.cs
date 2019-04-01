using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace ScientificReport.Models
{
	public class UserProfile : IdentityUser
	{
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
		
		public virtual ICollection<Publication> Publications { get; set; }
		
		public virtual ICollection<Grant> Grants { get; set; }
		
		public virtual ICollection<ScientificWork> ScientificWorks { get; set; }
	}
}
