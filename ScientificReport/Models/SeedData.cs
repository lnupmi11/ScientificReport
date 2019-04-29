using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScientificReport.DAL.DbContext;
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

			SeedUserProfile(context);
			await SeedUserRoles(serviceProvider.GetRequiredService<RoleManager<UserProfileRole>>(), logger);
			context.SaveChanges();
		}

		private static void SeedUserProfile(ScientificReportDbContext context)
		{
			/*
			if (context.UserProfile.Any()) return;

			context.UserProfile.AddRange(
				new UserProfile
				{
					FirstName = "Testf",
					LastName = "Testl",
					MiddleName = "Testm",
					Type = UserType.Admin,
				},
				new UserProfile
				{
					FirstName = "Testf2",
					LastName = "Testl2",
					MiddleName = "Testm2",
					Type = UserType.Admin,
				}
			);
			*/
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
