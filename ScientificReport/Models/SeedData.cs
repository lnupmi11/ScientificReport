using System;
using System.Collections.Generic;
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
using ScientificReport.DTO.Models.Membership;
using ScientificReport.DTO.Models.Opposition;
using ScientificReport.DTO.Models.PatentLicenseActivity;
using ScientificReport.DTO.Models.PostgraduateDissertationGuidance;
using ScientificReport.DTO.Models.PostgraduateGuidance;
using ScientificReport.DTO.Models.ReportThesis;
using ScientificReport.DTO.Models.ScientificConsultation;
using ScientificReport.DTO.Models.ScientificInternship;

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
		public static void Initialize(IServiceProvider serviceProvider, ScientificReportDbContext context)
		{
			var env = serviceProvider.GetService<IHostingEnvironment>();

			// prevents usage on any non-development environment
			if (!env.IsDevelopment()) return;

			var logger = serviceProvider.GetRequiredService<ILogger<SeedData>>();

			SeedUserRoles(serviceProvider.GetRequiredService<RoleManager<UserProfileRole>>(), logger).Wait();
			SeedUserProfile(context, serviceProvider).Wait();
			SeedDepartments(context, serviceProvider).Wait();
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

		private static async Task SeedUserProfile(ScientificReportDbContext context, IServiceProvider serviceProvider)
		{
			if (context.UserProfiles.Any()) return;

			var users = new[]
			{
				new UserProfile
				{
					Email = "orest@gmail.com",
					Position = "Teacher",
					LastName = "Гопяк",
					UserName = "orest",
					BirthYear = 1999,
					FirstName = "Орест",
					IsApproved = true,
					IsActive = true,
					MiddleName = "Романович",
					PhoneNumber = "+380000000001",
					AcademicStatus = "Бакалавр",
					ScientificDegree = "Бакалавр",
					YearDegreeGained = 2020,
					GraduationYear = 2020,
					Sex = UserProfile.SexValue.Male
				},
				new UserProfile
				{
					Email = "yura@gmail.com",
					Position = "Teacher",
					LastName = "Лісовський",
					UserName = "yura",
					BirthYear = 1999,
					FirstName = "Юрій",
					IsApproved = true,
					IsActive = true,
					MiddleName = "Юрійович",
					PhoneNumber = "+380000000002",
					AcademicStatus = "Бакалавр",
					ScientificDegree = "Бакалавр",
					YearDegreeGained = 2020,
					GraduationYear = 2020,
					Sex = UserProfile.SexValue.Male
				},
				new UserProfile
				{
					Email = "olena@gmail.com",
					Position = "Teacher",
					LastName = "Шипка",
					UserName = "olena",
					BirthYear = 1999,
					FirstName = "Олена",
					IsApproved = true,
					IsActive = true,
					MiddleName = "Романівна",
					PhoneNumber = "+380000000003",
					AcademicStatus = "Бакалавр",
					ScientificDegree = "Бакалавр",
					YearDegreeGained = 2020,
					GraduationYear = 2020,
					Sex = UserProfile.SexValue.Female
				},
				new UserProfile
				{
					Email = "roman@gmail.com",
					Position = "Teacher",
					LastName = "Кордюк",
					UserName = "roman",
					BirthYear = 1999,
					FirstName = "Роман",
					IsApproved = true,
					IsActive = true,
					MiddleName = "Ігорович",
					PhoneNumber = "+380000000004",
					AcademicStatus = "Бакалавр",
					ScientificDegree = "Бакалавр",
					YearDegreeGained = 2020,
					GraduationYear = 2020,
					Sex = UserProfile.SexValue.Male
				}
			};
			var userManager = serviceProvider.GetRequiredService<UserManager<UserProfile>>();
			var userService = new UserProfileService(context);
			foreach (var user in users)
			{
				await userManager.CreateAsync(user, "qwerty");
				if (user.UserName != "olena")
				{
					var usr = userService.Get(u => u.UserName == user.UserName);
					await userManager.AddToRolesAsync(usr, new[] {UserProfileRole.Teacher, UserProfileRole.Administrator});
				}
			}
		}
		
		private static async Task SeedDepartments(ScientificReportDbContext context, IServiceProvider serviceProvider)
		{
			if (context.Departments.Any()) return;

			var head = context.UserProfiles.First(u => u.UserName == "olena");
			var departments = new[]
			{
				new Department
				{
					Head = head,
					Staff = new List<UserProfile>
					{
						head,
						context.UserProfiles.First(u => u.UserName == "yura"),
						context.UserProfiles.First(u => u.UserName == "roman"),
						context.UserProfiles.First(u => u.UserName == "orest"),
					},
					Title = "Програмування"
				}
			};
			foreach (var department in departments)
			{
				context.Departments.Add(department);
			}
			
			var userManager = serviceProvider.GetRequiredService<UserManager<UserProfile>>();
			await userManager.AddToRolesAsync(head, new[] {UserProfileRole.Teacher, UserProfileRole.HeadOfDepartment});

			head.Position = UserProfileRole.HeadOfDepartment;
			
			await context.SaveChangesAsync();
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
					Type = Conference.Types.Local,
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
					ArticleType = Article.ArticleTypes.ImpactFactor,
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
			
			articleService.AddAuthor(
				articleService.Get(a => a.Title == "my first Article"),
				context.Users.First(u => u.UserName == "olena")
			);
		}

		private static void SeedPublications(ScientificReportDbContext context)
		{
			if (context.Publications.Any())
			{
				return;
			}

			var publicationsService = new PublicationService(context);

			if (context.UserProfiles.First() == null)
			{
				return;
			}

			publicationsService.CreateItem(new Publication
				{
					PublicationType = Publication.PublicationTypes.Monograph,
					Title = "my first publication",
					PublishingPlace = "my first publishing place",
					Specification = "some first specification",
					PublishingHouseName = "new oreo",
					PublishingYear = 1999,
					PagesAmount = 200,
					PrintStatus = Publication.PrintStatuses.IsPrintCanceled
				}
			);
			publicationsService.AddAuthor(
				publicationsService.Get(a => a.Title == "my first publication"),
				context.Users.First(u => u.UserName == "olena")
			);
			publicationsService.AddAuthor(
				publicationsService.Get(a => a.Title == "my first publication"),
				context.Users.First(u => u.UserName == "yura")
			);
			
			publicationsService.CreateItem(new Publication
				{
					PublicationType = Publication.PublicationTypes.TextBook,
					Title = "my second publication",
					PublishingPlace = "my second publishing place",
					Specification = "some second specification",
					PublishingHouseName = "new oreo",
					PublishingYear = 2019,
					PagesAmount = 300,
					PrintStatus = Publication.PrintStatuses.IsRecommendedToPrint
				}
			);

			publicationsService.AddAuthor(
				publicationsService.Get(a => a.Title == "my second publication"),
				context.Users.First(u => u.UserName == "olena")
			);
			
			publicationsService.AddAuthor(
				publicationsService.Get(a => a.Title == "my second publication"),
				context.Users.First(u => u.UserName == "yura")
			);
			publicationsService.AddAuthor(
				publicationsService.Get(a => a.Title == "my second publication"),
				context.Users.First(u => u.UserName == "roman")
			);
			publicationsService.CreateItem(new Publication
				{
					PublicationType = Publication.PublicationTypes.Comment,
					Title = "My comment",
					PublishingPlace = "Dnipro",
					Specification = "scientific",
					PublishingHouseName = "first publish house name",
					PublishingYear = 2015,
					PagesAmount = 300,
					PrintStatus = Publication.PrintStatuses.IsRecommendedToPrint
				}
			);
			publicationsService.CreateItem(new Publication
				{
					PublicationType = Publication.PublicationTypes.HandBook,
					Title = "My HandBook",
					PublishingPlace = "Lviv",
					Specification = "scientific",
					PublishingHouseName = "n-th publish name",
					PublishingYear = 2016,
					PagesAmount = 1500,
					PrintStatus = Publication.PrintStatuses.IsRecommendedToPrint
				}
			);
			publicationsService.CreateItem(new Publication
				{
					PublicationType = Publication.PublicationTypes.BibliographicIndex,
					Title = "My BibliographicIndex",
					PublishingPlace = "Ukraine",
					Specification = "bibliya",
					PublishingHouseName = "church",
					PublishingYear = 1,
					PagesAmount = 2000,
					PrintStatus = Publication.PrintStatuses.IsRecommendedToPrint
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

			scientificInternshipService.CreateItem(new ScientificInternshipModel(new ScientificInternship
			{
				PlaceOfInternship = "America",
				Started = DateTime.Today,
				Ended = DateTime.Now,
				Contents = "Good Job"
			}));
			scientificInternshipService.AddUser(
				context.ScientificInternships.FirstOrDefault(m => m.PlaceOfInternship == "PlaceOfInternship"),
				context.UserProfiles.FirstOrDefault(u => u.UserName == "yura"));
		}

		private static void SeedScientificConsultation(ScientificReportDbContext context)
		{
			if (context.ScientificConsultations.Any()) return;

			var scientificConsultationsService = new ScientificConsultationService(context);

			scientificConsultationsService.CreateItem(new ScientificConsultationModel(new ScientificConsultation
			{
				CandidateName = "Igor",
				DissertationTitle = "Work in Africa",
				Guide = context.UserProfiles.First(u => u.UserName == "orest")
			}));
			scientificConsultationsService.CreateItem(new ScientificConsultationModel(new ScientificConsultation
			{
				CandidateName = "Yura",
				DissertationTitle = "Work in Canada",
				Guide = context.UserProfiles.First(u => u.UserName == "roman")
			}));
		}

		private static void SeedReportThesis(ScientificReportDbContext context)
		{
			if (context.ReportTheses.Any()) return;

			var reportThesesService = new ReportThesisService(context);
			var conference = context.Conferences.First();

			reportThesesService.CreateItem(new ReportThesisModel(new ReportThesis
			{
				Thesis = "My first thesis",
				Conference = conference
			}));

			var reportThesis = reportThesesService.GetAll().First();
			var author = context.UserProfiles.First();
			
			reportThesesService.AddAuthor(reportThesis.Id, author.Id);
		}

		private static void SeedPostgraduateGuidance(ScientificReportDbContext context)
		{
			if (context.PostgraduateGuidances.Any()) return;

			var postgraduateGuidanceService = new PostgraduateGuidanceService(context);

			postgraduateGuidanceService.CreateItem(new PostgraduateGuidanceModel(new PostgraduateGuidance
			{
				PostgraduateName = "Bogdan Ivanovych",
				PostgraduateInfo = "now is working",
				Guide = context.UserProfiles.First(u => u.UserName == "orest")
			}));
		}

		private static void SeedPostgraduateDissertationGuidance(ScientificReportDbContext context)
		{
			if (context.PostgraduateDissertationGuidances.Any()) return;

			var postgraduateDissertationGuidanceService = new PostgraduateDissertationGuidanceService(context);

			postgraduateDissertationGuidanceService.CreateItem(new PostgraduateDissertationGuidanceModel(new PostgraduateDissertationGuidance
			{
				PostgraduateName = "Orest Romanovych",
				Dissertation = "big",
				Speciality = "math",
				DateDegreeGained = DateTime.Today,
				GraduationYear = 2012,
				Guide = context.UserProfiles.First(u => u.UserName == "yura")
			}));
		}

		private static void SeedPatentLicenseActivity(ScientificReportDbContext context)
		{
			if (context.PatentLicenseActivities.Any()) return;

			var patentLicenseActivityService = new PatentLicenseActivityService(context);

			patentLicenseActivityService.CreateItem(new PatentLicenseActivityModel(new PatentLicenseActivity
			{
				Name = "High",
				Number = 2,
				Date = DateTime.Now,
				Type = PatentLicenseActivity.Types.Application	
			}));
			
			patentLicenseActivityService.CreateItem(new PatentLicenseActivityModel(new PatentLicenseActivity
			{
				Name = "Medium",
				Number = 4,
				Date = DateTime.Today,
				Type = PatentLicenseActivity.Types.Patent	
			}));
		}
		
		private static void SeedOpposition(ScientificReportDbContext context)
		{
			if (context.Oppositions.Any()) return;

			var oppositionsService = new OppositionService(context);

			oppositionsService.CreateItem(
				new OppositionModel(new Opposition
				{
					About = "Nice opposition",
					DateOfOpposition = DateTime.Now,
					Opponent = context.UserProfiles.First(u => u.UserName == "yura")
				})
			);
			
			oppositionsService.CreateItem(
				new OppositionModel(new Opposition
				{
					About = "Bad opposition",
					DateOfOpposition = DateTime.Today,
					Opponent = context.UserProfiles.First(u => u.UserName == "olena")
				})
			);
		}

		private static void SeedMembership(ScientificReportDbContext context)
		{
			if(context.Memberships.Any()) return;
			
			var membershipService = new MembershipService(context);

			membershipService.CreateItem(new MembershipModel(new Membership
			{
				Type = Membership.Types.ScientificCouncil,
				MembershipInfo = "good helper",
				User = context.UserProfiles.First(u => u.UserName == "yura")
			}));
			membershipService.CreateItem(new MembershipModel(new Membership
			{
				Type = Membership.Types.ExpertCouncil,
				MembershipInfo = "best helper",
				User = context.UserProfiles.First(u => u.UserName == "orest")
			}));
			membershipService.CreateItem(new MembershipModel(new Membership
			{
				Type = Membership.Types.EditorialBoard,
				MembershipInfo = "normal guy",
				User = context.UserProfiles.First(u => u.UserName == "yura")
			}));
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
				if (await roleManager.RoleExistsAsync(roleName)) continue;
				var taskResult = await roleManager.CreateAsync(new UserProfileRole(roleName));
				if (!taskResult.Succeeded)
				{
					logger.LogWarning("Could not create role: " + roleName);
				}
			}
		}
	}
}