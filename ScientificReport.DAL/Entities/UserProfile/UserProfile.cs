using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace ScientificReport.DAL.Entities.UserProfile
{
	public class UserProfile : IdentityUser<Guid>
	{		
		public string FirstName { get; set; }
		
		public string MiddleName { get; set; }
		
		public string LastName { get; set; }
		
		public int BirthYear { get; set; }

		public int GraduationYear { get; set; }
		
		public string ScientificDegree { get; set; }
		
		public int YearDegreeGained { get; set; }
		
		public string AcademicStatus { get; set; }
		
		public int YearDegreeAssigned { get; set; }
		
		public string Position { get; set; }
		
		public bool IsApproved { get; set; }
		
		public bool IsActive { get; set; }
		
		public virtual ICollection<UserProfilesPublications> UserProfilesPublications { get; set; }
		
		public virtual ICollection<UserProfilesGrants> UserProfilesGrants { get; set; }
		
		public virtual ICollection<UserProfilesScientificWorks> UserProfilesScientificWorks{ get; set; }
		
		public virtual ICollection<UserProfilesArticles> UserProfilesArticles { get; set; }
		
		public virtual ICollection<UserProfilesReportThesis> UserProfilesReportTheses { get; set; }
		
		public virtual ICollection<UserProfilesReviews> UserProfilesReviews { get; set; }
		
		public virtual ICollection<UserProfilesScientificInternships> UserProfilesScientificInternships { get; set; }
		
		public virtual ICollection<AuthorsPatentLicenseActivities> AuthorsPatentLicenseActivities { get; set; }
		
		public virtual ICollection<ApplicantsPatentLicenseActivities> ApplicantsPatentLicenseActivities { get; set; }
		
		public string FullName => $"{LastName} {FirstName} {MiddleName}";
	}
}
