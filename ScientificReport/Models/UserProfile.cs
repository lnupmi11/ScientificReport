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
		
		public virtual ICollection<UserProfilesPublications> UserProfilesPublications { get; set; }
		
		public virtual ICollection<UserProfilesGrants> UserProfilesGrants { get; set; }
		
		public virtual ICollection<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
		
		public virtual ICollection<UserProfilesArticles> UserProfilesArticles { get; set; }
		
		public virtual ICollection<UserProfilesReportThesis> UserProfilesReportTheses { get; set; }
		
		public virtual ICollection<UserProfilesReviews> UserProfilesReviews { get; set; }
		
		public virtual ICollection<UserProfilesScientificInternships> UserProfilesScientificInternships { get; set; }
		
//		public virtual ICollection<UserProfilesPatentLicenseActivities> UserProfilesPatentLicenseActivities { get; set; }
	}
}
