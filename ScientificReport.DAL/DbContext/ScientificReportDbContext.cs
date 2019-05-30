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

		public virtual DbSet<UserProfile> UserProfiles { get; set; }
		public virtual DbSet<Article> Articles { get; set; }
		public DbSet<UserProfilesArticles> UserProfilesArticles { get; set; }
		public virtual DbSet<Publication> Publications { get; set; }
		public DbSet<UserProfilesPublications> UserProfilesPublications { get; set; }
		public virtual DbSet<Grant> Grants { get; set; }
		public DbSet<UserProfilesGrants> UserProfilesGrants { get; set; }
		public virtual DbSet<ScientificWork> ScientificWorks { get; set; }
		public DbSet<UserProfilesScientificWorks> UserProfilesScientificWorks { get; set; }
		public virtual DbSet<Review> Reviews { get; set; }
		public virtual DbSet<Conference> Conferences { get; set; }
		public virtual DbSet<ReportThesis> ReportTheses { get; set; }
		public DbSet<UserProfilesReportThesis> UserProfilesReportTheses { get; set; }
		public virtual DbSet<ScientificInternship> ScientificInternships { get; set; }
		public DbSet<UserProfilesScientificInternships> UserProfilesScientificInternships { get; set; }
		public virtual DbSet<Department> Departments { get; set; }
		public virtual DbSet<Membership> Memberships { get; set; }
		public virtual DbSet<Opposition> Oppositions { get; set; }
		public virtual DbSet<PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }
		public virtual DbSet<PostgraduateGuidance> PostgraduateGuidances { get; set; }
		public virtual DbSet<ScientificConsultation> ScientificConsultations { get; set; }
		public virtual DbSet<PatentLicenseActivity> PatentLicenseActivities { get; set; }
		public DbSet<ApplicantsPatentLicenseActivities> ApplicantsPatentLicenseActivities { get; set; }
		public DbSet<AuthorsPatentLicenseActivities> AuthorsPatentLicenseActivities { get; set; }

		public virtual DbSet<TeacherReport> TeacherReports { get; set; }
		public DbSet<TeacherReportsScientificWorks> TeacherReportsScientificWorks { get; set; }


		public virtual DbSet<DepartmentReport> DepartmentReports { get; set; }
		public virtual DbSet<FacultyReport> FacultyReports { get; set; }

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
