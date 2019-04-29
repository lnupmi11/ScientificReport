using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ScientificReport.DAL.DbContext;
using ScientificReport.Models;

namespace ScientificReport
{
	// ReSharper disable once ClassNeverInstantiated.Global
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateWebHostBuilder(args).Build();
			
			// just for automated migration and seeding on startup
			using (var scope = host.Services.CreateScope())
			{
				var serviceProvider = scope.ServiceProvider;

				try
				{
					var context = serviceProvider.GetRequiredService<ScientificReportDbContext>();
					context.Database.Migrate();
					SeedData.Initialize(serviceProvider, context).Wait();
				}
				catch (Exception ex)
				{
					var logger = serviceProvider.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred seeding the DB.");
				}
			}

			host.Run();
		}

		private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>();
	}
}
