using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Reports;
using Microsoft.Extensions.Logging;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Roles;

namespace ScientificReport.Models
{
	/// <summary>
	/// This class is created to seed the basic data to be used
	/// in both frontend and backend and
	/// it is for development purposes only
	/// </summary>
	public class SeedData
	{
		/// <summary>
		/// Initializes the basic data, the entrypoint for all seeds
		/// </summary>
		public static async Task Initialize(IServiceProvider serviceProvider, ScientificReportDbContext context)
		{
			var env = serviceProvider.GetService<IHostingEnvironment>();

			// prevents usage on any non-development environment
			if (!env.IsDevelopment()) return;

			var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();

      await SeedUserRoles(serviceProvider.GetRequiredService<RoleManager<UserProfileRole>>(), logger);
			SeedUserProfile(context);
			SeedTeacherReports(context);
			SeedScientificWorks(context);			
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
					MiddleName = "Testm"
				},
				new UserProfile
				{
					FirstName = "Testf2",
					LastName = "Testl2",
					MiddleName = "Testm2"
				}
			);
			context.SaveChanges();
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
			context.SaveChanges();
		}
		
		private static void SeedScientificWorks(ScientificReportDbContext context)
		{
			if (context.ScientificWorks.Any()) return;

			var scientificWorkService = new ScientificWorkService(context);
			scientificWorkService.CreateItem(
				new ScientificWork
				{
					Cypher = "123",
					Title = "Test SW",
					Category = "test",
					Contents = "blabla"
				}
			);
			var scientificWork = scientificWorkService.GetAll().First();
			var author = context.UserProfiles.First();
			
			scientificWorkService.AddAuthor(scientificWork.Id, author.Id);
		}
    
		private static async Task SeedUserRoles(RoleManager<UserProfileRole> roleManager, ILogger logger)
		{
			foreach (var roleName in UserProfileRole.Roles)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					var taskResult = await roleManager.CreateAsync(new UserProfileRole(roleName));
					if (!taskResult.Succeeded)
					{
						logger.LogWarning("Could not create role: " + roleName);
					}
				}
			}
		}
	}
}
