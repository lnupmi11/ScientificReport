using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScientificReport.Models;

namespace ScientificReport.Data
{
	public class ApplicationDbContext : IdentityDbContext<UserProfile>
	{
		public ApplicationDbContext(DbContextOptions options)
			: base(options)
		{
		}

		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<UserProfilesArticles> UserProfilesArticles { get; set; }
	//	public DbSet<PatentLicenseActivity> PatentLicenseActivities { get; set; }
	//	public DbSet<UserProfilesPatentLicenseActivities> UserProfilesPatentLicenseActivities { get; set; }
		public DbSet<Publication> Publications { get; set; }
		public DbSet<UserProfilesPublications> UserProfilesPublications { get; set; }
		public DbSet<Grant> Grants { get; set; }
		public DbSet<UserProfilesGrants> UserProfilesGrants { get; set; }
		public DbSet<ScientificWork> ScientificWorks { get; set; }
		public DbSet<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
		public DbSet<Conference> Conferences { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Membership> Membership { get; set; }
		public DbSet<Opposition> Oppositions { get; set; }
		public DbSet<PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }
		public DbSet<PostgraduateGuidance> PostgraduateGuidances { get; set; }
		public DbSet<ReportThesis> ReportTheses { get; set; }
		public DbSet<UserProfilesReportThesis> UserProfilesReportTheses { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<UserProfilesReviews> UserProfilesReviews { get; set; }
		public DbSet<ScientificConsultation> ScientificConsultations { get; set; }
		public DbSet<ScientificInternship> ScientificInternships { get; set; }
		public DbSet<UserProfilesScientificInternships> UserProfilesScientificInternships { get; set; }
		public DbSet<TeacherReport> TeacherReports { get; set; }
		public DbSet<DepartmentReport> DepartmentReports { get; set; }
		public DbSet<FacultyReport> FacultyReports { get; set; }

		/*
		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);
			builder.Entity<UserProfile>()
				.HasOne(u => u.UserProfilesPatentLicenseActivities)
				.WithMany(u => u.Authors);
			builder.Entity<UserProfile>()
				.HasOne(u => u.UserProfilesPatentLicenseActivities)
				.WithMany(u => u.Applicants);
		}
		*/
	}
}
