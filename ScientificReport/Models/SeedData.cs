using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
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
			SeedConference(context);
			SeedPublications(context);
			SeedArticle(context);
			SeedDepartment(context);
			SeedScientificInternship(context);
			SeedScientificConsultation(context);
			SeedReportThesis(context);
			SeedPostgraduateGuidance(context);
			SeedPostgraduateDissertationGuidance(context);
			SeedPatentLicenseActivity(context);
			SeedOpposition(context);
			SeedMembership(context);
			
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

		private static void SeedConference(ScientificReportDbContext context)
		{
			if (context.Conferences.Any()) return;

			var conferenceService = new ConferenceService(context);

			conferenceService.CreateItem(
				new Conference
				{
					Topic = "Best topic ever",
					Date = DateTime.Now
				}
			);
		}

		private static void SeedArticle(ScientificReportDbContext context)
		{
			if (context.Articles.Any()) return;

			var articleService = new ArticleService(context);

			articleService.CreateItem(
				new Article
				{
					Title = "my first Article",
					Type = Article.Types.ImpactFactor,
					PublishingPlace = "LNU",
					PublishingHouseName = "Lnu oreo",
					PublishingYear = 2019,
					PagesAmount = 250,
					IsPrintCanceled = true,
					IsRecommendedToPrint = false,
					LiabilityInfo = "some txt",
					DocumentInfo = "doc txt"
				}
			);
		}

		private static void SeedPublications(ScientificReportDbContext context)
		{
			if (context.Publications.Any()) return;

			var publicationsService = new PublicationService(context);
			
			if(context.UserProfiles.First() == null) return;
			var author = context.UserProfiles.First();


			publicationsService.CreateItem(author,
				new Publication
				{
					Type = Publication.Types.Monograph,
					Title = "my first publication",
					PublishingPlace = "my first publishing place",
					Specification = "some first specification",
					PublishingHouseName = "new oreo",
					PublishingYear = 1999,
					PagesAmount = 200,
					IsPrintCanceled = true,
					IsRecommendedToPrint = false,
					CreatedAt = DateTime.Today,
					LastEditAt = DateTime.Now
				}
			);

			publicationsService.CreateItem(author,
				new Publication
				{
					Type = Publication.Types.TextBook,
					Title = "my second publication",
					PublishingPlace = "my second publishing place",
					Specification = "some second specification",
					PublishingHouseName = "new oreo",
					PublishingYear = 2999,
					PagesAmount = 300,
					IsPrintCanceled = false,
					IsRecommendedToPrint = true,
					CreatedAt = DateTime.Today,
					LastEditAt = DateTime.Now
				}
			);
			
			publicationsService.CreateItem(author,
				new Publication
				{
					Type = Publication.Types.Comment,
					Title = "My comment",
					PublishingPlace = "Dnipro",
					Specification = "scientific",
					PublishingHouseName = "first publish house name",
					PublishingYear = 2015,
					PagesAmount = 300,
					IsPrintCanceled = false,
					IsRecommendedToPrint = true,
					CreatedAt = DateTime.Today,
					LastEditAt = DateTime.Now
				}
			);
			publicationsService.CreateItem(author,
				new Publication
				{
					Type = Publication.Types.HandBook,
					Title = "My HandBook",
					PublishingPlace = "Lviv",
					Specification = "scientific",
					PublishingHouseName = "n-th publish name",
					PublishingYear = 2016,
					PagesAmount = 1500,
					IsPrintCanceled = false,
					IsRecommendedToPrint = true,
					CreatedAt = DateTime.Today,
					LastEditAt = DateTime.Now
				}
			);
			publicationsService.CreateItem(author,
				new Publication
				{
					Type = Publication.Types.BibliographicIndex,
					Title = "My BibliographicIndex",
					PublishingPlace = "Ukraine",
					Specification = "bibliya",
					PublishingHouseName = "church",
					PublishingYear = 1,
					PagesAmount = 2000,
					IsPrintCanceled = false,
					IsRecommendedToPrint = true,
					CreatedAt = DateTime.Today,
					LastEditAt = DateTime.Now
				}
			);
			
		}

		private static void SeedDepartment(ScientificReportDbContext context)
		{
			if (context.Departments.Any()) return;

			var departmentService = new DepartmentService(context);
			
			if(context.ScientificWorks.Any()) return;
			
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
			
			departmentService.CreateItem(
				new Department
				{
					Title = "Programming"
				});
			
			var scientificWork = scientificWorkService.GetAll().First();
			departmentService.AddScientificWork(departmentService.GetAll().First().Id, scientificWork);
			
			if (!context.UserProfiles.Any()) return;
			var departmentUser = context.UserProfiles.First();
			departmentService.AddUser(departmentUser.Id, departmentUser);
			
		}

		private static void SeedScientificInternship(ScientificReportDbContext context)
		{
			if (context.ScientificInternships.Any()) return;

			var scientificInternshipService = new ScientificInternshipService(context);

			scientificInternshipService.CreateItem(
				new ScientificInternship
				{
					PlaceOfInternship = "America",
					Started = DateTime.Today,
					Ended = DateTime.Now,
					Contents = "Good Job",
				}
			);
		}

		private static void SeedScientificConsultation(ScientificReportDbContext context)
		{
			if (context.ScientificConsultations.Any()) return;

			var scientificConsultationsService = new ScientificConsultationService(context);

			scientificConsultationsService.CreateItem(
				new ScientificConsultation
				{
					Name = "First consultation",
					DissertationTitle = "Work in Africa"
				}
			);
			scientificConsultationsService.CreateItem(
				new ScientificConsultation
				{
					Name = "Second consultation",
					DissertationTitle = "Work in Canada"
				}
			);
		}

		private static void SeedReportThesis(ScientificReportDbContext context)
		{
			if (context.ReportTheses.Any()) return;

			var reportThesesService = new ReportThesisService(context);

			reportThesesService.CreateItem(
				new ReportThesis
				{
					Thesis = "My first thesis"
				}
			);
			reportThesesService.CreateItem(
				new ReportThesis()
				{
					Thesis = "My second thesis"
				}
			);
		}

		private static void SeedPostgraduateGuidance(ScientificReportDbContext context)
		{
			if (context.PostgraduateGuidances.Any()) return;

			var postgraduateGuidanceService = new PostgraduateGuidanceService(context);

			postgraduateGuidanceService.CreateItem(
				new PostgraduateGuidance
				{
					PostgraduateName = "Bogdan Ivanovych",
					PostgraduateInfo = "now is working"
				}
			);
		}

		private static void SeedPostgraduateDissertationGuidance(ScientificReportDbContext context)
		{
			if (context.PostgraduateDissertationGuidances.Any()) return;

			var postgraduateDissertationGuidanceService = new PostgraduateDissertationGuidanceService(context);

			postgraduateDissertationGuidanceService.CreateItem(
				new PostgraduateDissertationGuidance
				{
					PostgraduateName = "Orest Romanovych",
					Dissertation = "big",
					Speciality = "math",
					DateDegreeGained = DateTime.Today,
					GraduationYear = 2012
				}
			);
		}

		private static void SeedPatentLicenseActivity(ScientificReportDbContext context)
		{
			if (context.PatentLicenseActivities.Any()) return;

			var patentLicenseActivityService = new PatentLicenseActivityService(context);

			patentLicenseActivityService.CreateItem(
				new PatentLicenseActivity
				{
					Name = "High",
					Number = 2,
					DateTime = DateTime.Now,
					Type = PatentLicenseActivity.Types.Application	
				}
			);
			
			patentLicenseActivityService.CreateItem(
				new PatentLicenseActivity
				{
					Name = "Medium",
					Number = 4,
					DateTime = DateTime.Today,
					Type = PatentLicenseActivity.Types.Patent	
				}
			);
		}
		
		private static void SeedOpposition(ScientificReportDbContext context)
		{
			if (context.Oppositions.Any()) return;

			var oppositionsService = new OppositionService(context);

			oppositionsService.CreateItem(
				new Opposition
				{
					About = "Nice opposition",
					DateOfOpposition = DateTime.Now,
				}
			);
			
			oppositionsService.CreateItem(
				new Opposition
				{
					About = "Bad opposition",
					DateOfOpposition = DateTime.Today,
				}
			);
		}

		private static void SeedMembership(ScientificReportDbContext context)
		{
			if(context.Membership.Any()) return;
			
			var membershipService = new MembershipService(context);
			
			membershipService.CreateItem(
					new Membership
					{
						MemberOf = Membership.MemberOfChoices.ScientificCouncil,
						MembershipInfo = "good helper"
					}
				);
			membershipService.CreateItem(
				new Membership
				{
					MemberOf = Membership.MemberOfChoices.ExpertCouncil,
					MembershipInfo = "best helper"
				}
			);
			membershipService.CreateItem(
				new Membership
				{
					MemberOf = Membership.MemberOfChoices.EditorialBoard,
					MembershipInfo = "normal guy"
				}
			);
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