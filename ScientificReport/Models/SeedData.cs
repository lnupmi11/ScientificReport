using System;
using System.Linq;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Reports;

namespace ScientificReport.Models
{
	/// <summary>
	/// This class is created to seed the basic data to be used
	/// in both frontend and backend and
	/// it is for development purposes only
	/// </summary>
	public static class SeedData
	{
		/// <summary>
		/// Initializes the basic data, the entrypoint for all seeds
		/// </summary>
		/// <param name="serviceProvider"></param>
		public static void Initialize(IServiceProvider serviceProvider, ScientificReportDbContext context)
		{
			var env = serviceProvider.GetService<IHostingEnvironment>();

			// prevents usage on any non-development environment
			if (!env.IsDevelopment()) return;

			SeedUserProfile(context);
			SeedTeacherReports(context);
			context.SaveChanges();
		}

		private static void SeedUserProfile(ScientificReportDbContext context)
		{
			if (context.UserProfiles.Any()) return;

			context.UserProfiles.AddRange(
				new UserProfile
				{
					FirstName = "Testf",
					LastName = "Testl",
					MiddleName = "Testm",
				},
				new UserProfile
				{
					FirstName = "Testf2",
					LastName = "Testl2",
					MiddleName = "Testm2",
				}
			);
		}

		private static void SeedTeacherReports(ScientificReportDbContext context)
		{
			if (context.TeacherReports.Any()) return;
			var teacher = context.UserProfiles.First();

			context.TeacherReports.AddRange(
				new TeacherReport
				{
					Teacher = teacher
				}
			);
		}
	}
}