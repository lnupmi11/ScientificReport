using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Roles;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.DbContext
{
	public class ScientificReportDbContext : IdentityDbContext<UserProfile, UserProfileRole, Guid>
	{
		public ScientificReportDbContext()
		{
		}

		public ScientificReportDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<UserProfile> UserProfiles { get; set; }
		public DbSet<Article> Articles { get; set; }
		public DbSet<UserProfilesArticles> UserProfilesArticles { get; set; }
		public virtual DbSet<Publication> Publications { get; set; }
		public DbSet<UserProfilesPublications> UserProfilesPublications { get; set; }
		public DbSet<Grant> Grants { get; set; }
		public DbSet<UserProfilesGrants> UserProfilesGrants { get; set; }
		public DbSet<ScientificWork> ScientificWorks { get; set; }
		public DbSet<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
		public DbSet<Review> Reviews { get; set; }
		public DbSet<UserProfilesReviews> UserProfilesReviews { get; set; }
		public DbSet<Conference> Conferences { get; set; }
		public DbSet<ReportThesis> ReportTheses { get; set; }
		public DbSet<UserProfilesReportThesis> UserProfilesReportTheses { get; set; }
		public DbSet<ScientificInternship> ScientificInternships { get; set; }
		public DbSet<UserProfilesScientificInternships> UserProfilesScientificInternships { get; set; }
		public DbSet<Department> Departments { get; set; }
		public DbSet<Membership> Membership { get; set; }
		public DbSet<Opposition> Oppositions { get; set; }
		public DbSet<PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }
		public DbSet<PostgraduateGuidance> PostgraduateGuidances { get; set; }
		public DbSet<ScientificConsultation> ScientificConsultations { get; set; }
		public DbSet<PatentLicenseActivity> PatentLicenseActivities { get; set; }
		public DbSet<ApplicantsPatentLicenseActivities> ApplicantsPatentLicenseActivities { get; set; }
		public DbSet<AuthorsPatentLicenseActivities> AuthorsPatentLicenseActivities { get; set; }

		public DbSet<TeacherReport> TeacherReports { get; set; }
		public DbSet<TeacherReportsScientificWorks> TeacherReportsScientificWorks { get; set; }


		public DbSet<DepartmentReport> DepartmentReports { get; set; }
		public DbSet<FacultyReport> FacultyReports { get; set; }

		public override int SaveChanges()
		{
			AddTimestamps();
			return base.SaveChanges();
		}

		public override async Task<int> SaveChangesAsync(
			CancellationToken cancellationToken = default(CancellationToken))
		{
			AddTimestamps();
			return await base.SaveChangesAsync(cancellationToken);
		}

		private void AddTimestamps()
		{
			var entities = ChangeTracker.Entries().Where(x =>
				x.Entity is ITrackable && (x.State == EntityState.Added || x.State == EntityState.Modified));

			foreach (var entity in entities)
			{
				if (entity.State == EntityState.Added)
				{
					((ITrackable) entity.Entity).Created = DateTime.UtcNow;
				}

				((ITrackable) entity.Entity).Edited = DateTime.UtcNow;
			}
		}
	}
}
