using System;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Test
{
	public static class TestData
	{
		public static readonly UserProfile User1 = new UserProfile
		{
			Id = Guid.NewGuid(),
			Email = "email1@gmail.com",
			Position = "Some position1",
			LastName = "LastName1",
			UserName = "User_name_1",
			BirthYear = 1991,
			FirstName = "FirstName1",
			IsApproved = true,
			MiddleName = "MiddleName1",
			PhoneNumber = "+380000000001",
		};
		public static readonly UserProfile User2 = new UserProfile
		{
			Id = Guid.NewGuid(),
			Email = "email2@gmail.com",
			Position = "Some position2",
			LastName = "LastName2",
			UserName = "User_name_2",
			BirthYear = 1992,
			FirstName = "FirstName2",
			IsApproved = true,
			MiddleName = "MiddleName2",
			PhoneNumber = "+380000000002",
		};
		public static readonly UserProfile User3 = new UserProfile
		{
			Id = Guid.NewGuid(),
			Email = "email3@gmail.com",
			Position = "Some position3",
			LastName = "LastName3",
			UserName = "User_name_3",
			BirthYear = 1993,
			FirstName = "FirstName3",
			IsApproved = true,
			MiddleName = "MiddleName3",
			PhoneNumber = "+380000000003",
		};
		
		public static readonly ScientificWork ScientificWork1 = new ScientificWork
		{
			Id = Guid.NewGuid(),
			Title = "Scientific Work 1",
			Cypher = "6546ttt7",
			Category = "Category 1",
			Contents = "Some content 1"
		};
		public static readonly ScientificWork ScientificWork2 = new ScientificWork
		{
			Id = Guid.NewGuid(),
			Title = "Scientific Work 2",
			Cypher = "jhg567th",
			Category = "Category 2",
			Contents = "Some content 2"
		};
		public static readonly ScientificWork ScientificWork3 = new ScientificWork
		{
			Id = Guid.NewGuid(),
			Title = "Scientific Work 3",
			Cypher = "8765rfgh",
			Category = "Category 3",
			Contents = "Some content 3"
		};

		public static readonly Publication Publication1 = new Publication
		{
			Id = Guid.NewGuid(),
			Type = Publication.Types.Comment,
			Title = "Publication 1",
			Specification = "Specification 1",
			PagesAmount = 10,
			PublishingYear = 2007,
			PublishingPlace = "Publishing Place 1",
			PublishingHouseName = "Publishing House Name 1",
			PrintStatus = Publication.PrintStatuses.IsRecommendedToPrint
		};
		public static readonly Publication Publication2 = new Publication
		{
			Id = Guid.NewGuid(),
			Type = Publication.Types.Monograph,
			Title = "Publication 2",
			Specification = "Specification 2",
			PagesAmount = 20,
			PublishingYear = 2014,
			PublishingPlace = "Publishing Place 2",
			PublishingHouseName = "Publishing House Name 2",
			PrintStatus = Publication.PrintStatuses.IsPrintCanceled
		};
		public static readonly Publication Publication3 = new Publication
		{
			Id = Guid.NewGuid(),
			Type = Publication.Types.Dictionary,
			Title = "Publication 3",
			Specification = "Specification 3",
			PagesAmount = 30,
			PublishingYear = 2007,
			PublishingPlace = "Publishing Place 3",
			PublishingHouseName = "Publishing House Name 3",
			PrintStatus = Publication.PrintStatuses.Any
		};

		public static readonly Department Department1 = new Department
		{
			Id = Guid.NewGuid(),
			Head = User1,
			Staff = new List<UserProfile>
			{
				User1, User2
			},
			Title = "Some title 1",
			ScientificWorks = new List<ScientificWork>
			{
				ScientificWork1
			}
		};
		public static readonly Department Department2 = new Department
		{
			Id = Guid.NewGuid(),
			Head = User2,
			Staff = new List<UserProfile>
			{
				User2, User3
			},
			Title = "Some title 2",
			ScientificWorks = new List<ScientificWork>
			{
				ScientificWork2, 
			}
		};
		public static readonly Department Department3 = new Department
		{
			Id = Guid.NewGuid(),
			Head = User3,
			Staff = new List<UserProfile>
			{
				User1, User2, User3
			},
			Title = "Some title 3",
			ScientificWorks = new List<ScientificWork>
			{
				ScientificWork3
			}
		};

		public static readonly Membership Membership1 = new Membership
		{
			Id = Guid.NewGuid(),
			Type = Membership.Types.ExpertCouncil,
			MembershipInfo = "Some membership info 1",
			User = User1
		};
		public static readonly Membership Membership2 = new Membership
		{
			Id = Guid.NewGuid(),
			Type = Membership.Types.EditorialBoard,
			MembershipInfo = "Some membership info 2",
			User = User1
		};
		public static readonly Membership Membership3 = new Membership
		{
			Id = Guid.NewGuid(),
			Type = Membership.Types.ScientificCouncil,
			MembershipInfo = "Some membership info 3"
		};

		public static readonly Opposition Opposition1 = new Opposition
		{
			Id = Guid.NewGuid(),
			Opponent = User1,
			About = "About info 1",
			DateOfOpposition = DateTime.Today
		};
		public static readonly Opposition Opposition2 = new Opposition
		{
			Id = Guid.NewGuid(),
			Opponent = User1,
			About = "About info 2",
			DateOfOpposition = DateTime.Today
		};
		public static readonly Opposition Opposition3 = new Opposition
		{
			Id = Guid.NewGuid(),
			Opponent = User3,
			About = "About info 3",
			DateOfOpposition = DateTime.Today
		};

		public static readonly Article Article1 = new Article
		{
			Id = Guid.NewGuid(),
			Title = "Some title 1",
			IsPrintCanceled = true,
			UserProfilesArticles = null,
			Type = Article.Types.ReportThesis,
			Number = 1,
			PagesAmount = 20,
			DocumentInfo = "Some document info 1",
			IsPeriodical = true,
			LiabilityInfo = "Some liability info 1",
			PublishingYear = 2003,
			PublishingPlace = "Some place 1",
			PublishingHouseName = "Some house name 1",
			IsRecommendedToPrint = false
		};
		public static readonly Article Article2 = new Article
		{
			Id = Guid.NewGuid(),
			Title = "Some title 2",
			IsPrintCanceled = true,
			UserProfilesArticles = null,
			Type = Article.Types.ForeignPublishing,
			Number = 0,
			PagesAmount = 30,
			DocumentInfo = "Some document info 2",
			IsPeriodical = false,
			LiabilityInfo = "Some liability info 2",
			PublishingYear = 2004,
			PublishingPlace = "Some place 2",
			PublishingHouseName = "Some house name 2",
			IsRecommendedToPrint = false
		};
		public static readonly Article Article3 = new Article
		{
			Id = Guid.NewGuid(),
			Title = "Some title 3",
			IsPrintCanceled = false,
			UserProfilesArticles = null,
			Type = Article.Types.OtherPublishingOfUkraine,
			Number = 2,
			PagesAmount = 10,
			DocumentInfo = "Some document info 3",
			IsPeriodical = true,
			LiabilityInfo = "Some liability info 3",
			PublishingYear = 2005,
			PublishingPlace = "Some place 3",
			PublishingHouseName = "Some house name 3",
			IsRecommendedToPrint = true
		};

		public static readonly Grant Grant1 = new Grant
		{
			Id = Guid.NewGuid()
		};
		public static readonly Grant Grant2 = new Grant
		{
			Id = Guid.NewGuid()
		};
		public static readonly Grant Grant3 = new Grant
		{
			Id = Guid.NewGuid()
		};

		public static readonly Conference Conference1 = new Conference
		{
			Id = Guid.NewGuid(),
			Date = DateTime.Now,
			Topic = "Some topic 1"
		};
		public static readonly Conference Conference2 = new Conference
		{
			Id = Guid.NewGuid(),
			Date = DateTime.Now,
			Topic = "Some topic 2"
		};
		public static readonly Conference Conference3 = new Conference
		{
			Id = Guid.NewGuid(),
			Date = DateTime.Now,
			Topic = "Some topic 3"
		};
		
		public static readonly PatentLicenseActivity PatentLicenseActivity1 = new PatentLicenseActivity
		{
			Id = Guid.NewGuid(),
			Name = "Some name 1",
			Type = PatentLicenseActivity.Types.Patent,
			Number = 1,
			Date = DateTime.Now, 
		};
		public static readonly PatentLicenseActivity PatentLicenseActivity2 = new PatentLicenseActivity
		{
			Id = Guid.NewGuid(),
			Name = "Some name 2",
			Type = PatentLicenseActivity.Types.Patent,
			Number = 2,
			Date = DateTime.Now
		};
		public static readonly PatentLicenseActivity PatentLicenseActivity3 = new PatentLicenseActivity
		{
			Id = Guid.NewGuid(),
			Name = "Some name 3",
			Type = PatentLicenseActivity.Types.Patent,
			Number = 3,
			Date = DateTime.Now
		};

		public static readonly Review Review1 = new Review
		{
			Id = Guid.NewGuid(),
			Work = Publication1,
			DateOfReview = DateTime.Today
		};
		public static readonly Review Review2 = new Review
		{
			Id = Guid.NewGuid(),
			Work = Publication2,
			DateOfReview = DateTime.Today
		};
		public static readonly Review Review3 = new Review
		{
			Id = Guid.NewGuid(),
			Work = Publication3,
			DateOfReview = DateTime.Today
		};

		public static readonly PostgraduateGuidance PostgraduateGuidance1 = new PostgraduateGuidance
		{
			Id = Guid.NewGuid(),
			Guide = User1,
			PostgraduateInfo = "Postgraduate Info 1",
			PostgraduateName = "Postgraduate Name 1"
		};
		public static readonly PostgraduateGuidance PostgraduateGuidance2 = new PostgraduateGuidance
		{
			Id = Guid.NewGuid(),
			Guide = User2,
			PostgraduateInfo = "Postgraduate Info 2",
			PostgraduateName = "Postgraduate Name 2"
		};
		public static readonly PostgraduateGuidance PostgraduateGuidance3 = new PostgraduateGuidance
		{
			Id = Guid.NewGuid(),
			Guide = User3,
			PostgraduateInfo = "Postgraduate Info 3",
			PostgraduateName = "Postgraduate Name 3"
		};

		public static readonly PostgraduateDissertationGuidance PostgraduateDissertationGuidance1 =
			new PostgraduateDissertationGuidance
			{
				Id = Guid.NewGuid(),
				Guide = User1,
				GraduationYear = 2001,
				PostgraduateName = "Postgraduate Name 1",
				Speciality = "Speciality 1",
				Dissertation = "Dissertation 1",
				DateDegreeGained = DateTime.Now
			};
		public static readonly PostgraduateDissertationGuidance PostgraduateDissertationGuidance2 =
			new PostgraduateDissertationGuidance
			{
				Id = Guid.NewGuid(),
				Guide = User2,
				GraduationYear = 2002,
				PostgraduateName = "Postgraduate Name 2",
				Speciality = "Speciality 2",
				Dissertation = "Dissertation 2",
				DateDegreeGained = DateTime.Now
			};
		public static readonly PostgraduateDissertationGuidance PostgraduateDissertationGuidance3 =
			new PostgraduateDissertationGuidance
			{
				Id = Guid.NewGuid(),
				Guide = User3,
				GraduationYear = 2003,
				PostgraduateName = "Postgraduate Name 3",
				Speciality = "Speciality 3",
				Dissertation = "Dissertation 3",
				DateDegreeGained = DateTime.Now
			};

		public static readonly ScientificInternship ScientificInternship1 = new ScientificInternship
		{
			Id = Guid.NewGuid(),
			Contents = "Some content 1",
			Ended = DateTime.Now,
			Started = DateTime.Now,
			PlaceOfInternship = "Some place 1"
		};
		public static readonly ScientificInternship ScientificInternship2 = new ScientificInternship
		{
			Id = Guid.NewGuid(),
			Contents = "Some content 2",
			Ended = DateTime.Now,
			Started = DateTime.Now,
			PlaceOfInternship = "Some place 2"
		};
		public static readonly ScientificInternship ScientificInternship3 = new ScientificInternship
		{
			Id = Guid.NewGuid(),
			Contents = "Some content 3",
			Ended = DateTime.Now,
			Started = DateTime.Now,
			PlaceOfInternship = "Some place 3"
		};

		public static readonly ScientificConsultation ScientificConsultation1 = new ScientificConsultation
		{
			Id = Guid.NewGuid(),
			CandidateName = "Some name 1",
			Guide = User1,
			DissertationTitle = "Dissertation Title 1"
		};
		public static readonly ScientificConsultation ScientificConsultation2 = new ScientificConsultation
		{
			Id = Guid.NewGuid(),
			CandidateName = "Some name 2",
			Guide = User2,
			DissertationTitle = "Dissertation Title 2"
		};
		public static readonly ScientificConsultation ScientificConsultation3 = new ScientificConsultation
		{
			Id = Guid.NewGuid(),
			CandidateName = "Some name 3",
			Guide = User3,
			DissertationTitle = "Dissertation Title 3"
		};

		public static readonly ReportThesis ReportThesis1 = new ReportThesis
		{
			Id = Guid.NewGuid(),
			Conference = Conference1,
			Thesis = "Some thesis 1",
		};
		public static readonly ReportThesis ReportThesis2 = new ReportThesis
		{
			Id = Guid.NewGuid(),
			Conference = Conference1,
			Thesis = "Some thesis 2"
		};
		public static readonly ReportThesis ReportThesis3 = new ReportThesis
		{
			Id = Guid.NewGuid(),
			Conference = Conference3,
			Thesis = "Some thesis 3"
		};

		public static readonly TeacherReport TeacherReport1 = new TeacherReport
		{
			Id = Guid.NewGuid(),
			Edited = DateTime.Now,
//			Grants = new List<Grant>
//			{
//				Grant1
//			},
			Created = DateTime.Now,
			Teacher = User1,
//			Publications = new List<Publication>
//			{
//				Publication1, Publication2
//			},
//			ScientificWorks = new List<ScientificWork>
//			{
//				ScientificWork1
//			},
//			Patents = new List<PatentLicenseActivity>
//			{
//				PatentLicenseActivity1
//			},
//			Reviews = new List<Review>
//			{
//				Review1
//			},
//			Memberships = new List<Membership>
//			{
//				Membership1
//			},
//			Oppositions = new List<Opposition>
//			{
//				Opposition1
//			},
//			ReportTheses = new List<ReportThesis>(),
//			PostgraduateGuidances = new List<PostgraduateGuidance>
//			{
//				PostgraduateGuidance1
//			},
//			ScientificInternships = new List<ScientificInternship>
//			{
//				ScientificInternship1
//			},
//			ScientificConsultations = new List<ScientificConsultation>
//			{
//				ScientificConsultation1
//			},
//			PostgraduateDissertationGuidances = new List<PostgraduateDissertationGuidance>
//			{
//				PostgraduateDissertationGuidance1
//			}
		};
		public static readonly TeacherReport TeacherReport2 = new TeacherReport
		{
			Id = Guid.NewGuid(),
			Edited = DateTime.Now,
//			Grants = new List<Grant>
//			{
//				Grant2
//			},
			Created = DateTime.Now,
			Teacher = User2,
//			Publications = new List<Publication>
//			{
//				Publication2, Publication3
//			},
//			ScientificWorks = new List<ScientificWork>
//			{
//				ScientificWork2
//			},
//			Patents = new List<PatentLicenseActivity>
//			{
//				PatentLicenseActivity2
//			},
//			Reviews = new List<Review>
//			{
//				Review2
//			},
//			Memberships = new List<Membership>
//			{
//				Membership2
//			},
//			Oppositions = new List<Opposition>
//			{
//				Opposition2
//			},
//			ReportTheses = new List<ReportThesis>(),
//			PostgraduateGuidances = new List<PostgraduateGuidance>
//			{
//				PostgraduateGuidance2
//			},
//			ScientificInternships = new List<ScientificInternship>
//			{
//				ScientificInternship2
//			},
//			ScientificConsultations = new List<ScientificConsultation>
//			{
//				ScientificConsultation2
//			},
//			PostgraduateDissertationGuidances = new List<PostgraduateDissertationGuidance>
//			{
//				PostgraduateDissertationGuidance2
//			}
		};
		public static readonly TeacherReport TeacherReport3 = new TeacherReport
		{
			Id = Guid.NewGuid(),
			Edited = DateTime.Now,
//			Grants = new List<Grant>
//			{
//				Grant3
//			},
			Created = DateTime.Now,
			Teacher = User3,
//			Publications = new List<Publication>
//			{
//				Publication1
//			},
//			ScientificWorks = new List<ScientificWork>
//			{
//				ScientificWork3
//			},
//			Patents = new List<PatentLicenseActivity>
//			{
//				PatentLicenseActivity3
//			},
//			Reviews = new List<Review>
//			{
//				Review3
//			},
//			Memberships = new List<Membership>
//			{
//				Membership3
//			},
//			Oppositions = new List<Opposition>
//			{
//				Opposition3
//			},
//			ReportTheses = new List<ReportThesis>(),
//			PostgraduateGuidances = new List<PostgraduateGuidance>
//			{
//				PostgraduateGuidance3
//			},
//			ScientificInternships = new List<ScientificInternship>
//			{
//				ScientificInternship3
//			},
//			ScientificConsultations = new List<ScientificConsultation>
//			{
//				ScientificConsultation3
//			},
//			PostgraduateDissertationGuidances = new List<PostgraduateDissertationGuidance>
//			{
//				PostgraduateDissertationGuidance3
//			}
		};

		public static readonly DepartmentReport DepartmentReport1 = new DepartmentReport
		{
			Id = Guid.NewGuid(),
			Created = DateTime.Now,
			Edited = DateTime.Now,
			HeadOfDepartment = User1,
			Conferences = new List<Conference>
			{
				Conference1
			},
			TeacherReports = new List<TeacherReport>
			{
				TeacherReport1, TeacherReport2
			}
		};
		public static readonly DepartmentReport DepartmentReport2 = new DepartmentReport
		{
			Id = Guid.NewGuid(),
			Created = DateTime.Now,
			Edited = DateTime.Now,
			HeadOfDepartment = User2,
			Conferences = new List<Conference>
			{
				Conference2
			},
			TeacherReports = new List<TeacherReport>
			{
				TeacherReport1, TeacherReport3
			}
		};
		public static readonly DepartmentReport DepartmentReport3 = new DepartmentReport
		{
			Id = Guid.NewGuid(),
			Created = DateTime.Now,
			Edited = DateTime.Now,
			HeadOfDepartment = User3,
			Conferences = new List<Conference>
			{
				Conference3
			},
			TeacherReports = new List<TeacherReport>
			{
				TeacherReport2
			}
		};
		
		public static readonly FacultyReport FacultyReport1 = new FacultyReport
		{
			Id = Guid.NewGuid(),
			Administrator = User1,
			Edited = DateTime.Now,
			Created = DateTime.Now,
			DepartmentReports = new List<DepartmentReport>
			{
				DepartmentReport1, DepartmentReport2
			}
		};
		public static readonly FacultyReport FacultyReport2 = new FacultyReport
		{
			Id = Guid.NewGuid(),
			Administrator = User2,
			Edited = DateTime.Now,
			Created = DateTime.Now,
			DepartmentReports = new List<DepartmentReport>
			{
				DepartmentReport1
			}
		};
		public static readonly FacultyReport FacultyReport3 = new FacultyReport
		{
			Id = Guid.NewGuid(),
			Administrator = User3,
			Edited = DateTime.Now,
			Created = DateTime.Now,
			DepartmentReports = new List<DepartmentReport>
			{
				DepartmentReport3, DepartmentReport2
			}
		};

		private static readonly Random Rand = new Random();

		private static readonly string[] Words =
		{
			"Lorem", "Ipsum", "Dolor", "Sit", "Amet", "Consectetuer",
			"Adipiscing", "Elit", "Sed", "Diam", "Nonummy", "Nibh", "Euismod",
			"Tincidunt", "Ut", "Laoreet", "Dolore", "Magna", "Aliquam", "Erat"
		};

		private static string RandWord()
		{
			return Words[Rand.Next(Words.Length)];
		}
		
		private static string RandText(int wordsNumber)
		{
			var text = "";
			for (var i = 0; i < wordsNumber; i++)
			{
				text += RandWord() + " ";
			}

			return text;
		}

		private static bool RandBool()
		{
			return Rand.Next(2) == 1;
		}

		public static UserProfile RandUser(bool setApproved = false)
		{
			var fName = RandWord();
			var lName = RandWord();
			var uName = $"{fName.ToLower()}_{lName.ToLower()}";
			return new UserProfile
			{
				Id = Guid.NewGuid(),
				Email = $"{uName}@email.com",
				Position = RandWord(),
				LastName = lName,
				UserName = uName,
				BirthYear = Rand.Next(1920, 1996),
				FirstName = fName,
				IsApproved = setApproved || RandBool(),
				MiddleName = "MiddleName",
				PhoneNumber = $"+380{Rand.Next(100000000, 999999999)}",
			};
		}

		public static ScientificWork RandScientificWork()
		{
			return new ScientificWork
			{
				Id = Guid.NewGuid(),
				Title = RandText(3),
				Cypher = Rand.Next(100000, 99999).ToString(),
				Category = RandWord(),
				Contents = RandText(7)
			};
		}

		public static Publication RandPublication()
		{
			Publication.PrintStatuses printStatus;
			switch (Rand.Next(2))
			{
				case 1:
					printStatus = Publication.PrintStatuses.IsPrintCanceled;
					break;
				case 2:
					printStatus = Publication.PrintStatuses.IsRecommendedToPrint;
					break;
				default:
					printStatus = Publication.PrintStatuses.Any;
					break;
			}
			return new Publication
			{
				Id = Guid.NewGuid(),
				Type = Publication.Types.Comment,
				Title = RandText(3),
				Specification = RandWord(),
				PagesAmount = Rand.Next(3, 100),
				PublishingYear = Rand.Next(1990, 2018),
				PublishingPlace = RandText(4),
				PublishingHouseName = RandText(4),
				PrintStatus = printStatus
			};
		}
	}
}
