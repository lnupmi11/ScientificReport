using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using ScientificReport.BLL.Interfaces;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Roles;

namespace ScientificReport
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		private IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.Configure<CookiePolicyOptions>(options =>
			{
				// This lambda determines whether user consent for non-essential cookies is needed for a given request.
				options.CheckConsentNeeded = context => true;
				options.MinimumSameSitePolicy = SameSiteMode.None;
			});

			services.AddDbContext<ScientificReportDbContext>(options =>
				options.UseSqlite(Configuration.GetConnectionString("DefaultConnection"),
					b => b.MigrationsAssembly("ScientificReport"))
			);

			services.AddIdentity<UserProfile, UserProfileRole>()
					.AddEntityFrameworkStores<ScientificReportDbContext>()
					.AddDefaultTokenProviders();
			
			services.AddTransient<IUserProfileService, UserProfileService>();
			services.AddTransient<IDepartmentService, DepartmentService>();
			services.AddTransient<IScientificWorkService, ScientificWorkService>();
			services.AddTransient<ITeacherReportService, TeacherReportService>();
			services.AddTransient<IArticleService, ArticleService>();
			services.AddTransient<IConferenceService, ConferenceService>();
			services.AddTransient<IGrantService, GrantService>();
			services.AddTransient<IMembershipService, MembershipService>();
			services.AddTransient<IOppositionService, OppositionService>();
			services.AddTransient<IPatentLicenseActivityService, PatentLicenseActivityService>();
			services.AddTransient<IPostgraduateDissertationGuidanceService, PostgraduateDissertationGuidanceService>();
			services.AddTransient<IPostgraduateGuidanceService, PostgraduateGuidanceService>();
			services.AddTransient<IReportThesisService, ReportThesisService>();
			services.AddTransient<IReviewService, ReviewService>();
			services.AddTransient<IScientificConsultationService, ScientificConsultationService>();
			services.AddTransient<IScientificInternshipService, ScientificInternshipService>();
			services.AddTransient<IPublicationService, PublicationService>();

			services.Configure<IdentityOptions>(options =>
			{
				// Password settings.
				// TODO: configure password setting properly for deploy
				options.Password.RequireDigit = false;
				options.Password.RequireLowercase = false;
				options.Password.RequireNonAlphanumeric = false;
				options.Password.RequireUppercase = false;
				options.Password.RequiredLength = 3;
				options.Password.RequiredUniqueChars = 0;

				// Lockout settings.
				options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromDays(1);
				options.Lockout.MaxFailedAccessAttempts = 5;
				options.Lockout.AllowedForNewUsers = true;

				// User settings.
				options.User.AllowedUserNameCharacters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-._";
				options.User.RequireUniqueEmail = true;
			});

			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
			services.ConfigureApplicationCookie(options =>
			{
				// Cookie settings
				options.Cookie.HttpOnly = true;
				options.ExpireTimeSpan = TimeSpan.FromDays(1);
				options.LoginPath = "/UserProfile/Login";
				options.AccessDeniedPath = "/Home/AccessDenied";
				options.SlidingExpiration = true;
			});
			
			services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
			services.Configure<RequestLocalizationOptions>(
				opts =>
				{
					var supportedCultures = new List<CultureInfo>
					{
						new CultureInfo("uk-UA"),
						new CultureInfo("en")
					};

					opts.DefaultRequestCulture = new RequestCulture("uk-UA", "uk-UA");
					// Formatting numbers, dates, etc.
					opts.SupportedCultures = supportedCultures;
					// UI strings that we have localized.
					opts.SupportedUICultures = supportedCultures;
				});

			services.AddMvc()
				.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
				.AddViewLocalization(
					LanguageViewLocationExpanderFormat.SubFolder,
					opts => { opts.ResourcesPath = "Resources"; })
				.AddDataAnnotationsLocalization();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseCookiePolicy();
			
			app.UseAuthentication();

			var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
			app.UseRequestLocalization(options.Value);
			
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					"default",
					"{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
