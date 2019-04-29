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
				.ConfigureLogging((context, logging) => {
					var config = context.Configuration.GetSection("Logging");
					logging.AddConfiguration(config);
					logging.AddConsole();
					logging.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Warning);
					logging.AddFilter("Microsoft.AspNetCore.HttpsPolicy", LogLevel.Error);
					logging.AddFilter("Microsoft.AspNetCore.DataProtection.KeyManagement.XmlKeyManager", LogLevel.Warning);
					logging.AddFilter("Microsoft.AspNetCore.Hosting.Internal.WebHost", LogLevel.Warning);
					logging.AddFilter("Microsoft.AspNetCore.StaticFiles.StaticFileMiddleware", LogLevel.Warning);
//					logging.AddFilter("Microsoft.AspNetCore.Mvc.Internal.ControllerActionInvoker", LogLevel.Warning);
					logging.AddFilter("Microsoft.AspNetCore.Mvc.ViewFeatures.ViewResultExecutor", LogLevel.Warning);
					logging.AddFilter("Microsoft.AspNetCore.Mvc.Formatters", LogLevel.Warning);
				})
				.UseStartup<Startup>();
	}
}
