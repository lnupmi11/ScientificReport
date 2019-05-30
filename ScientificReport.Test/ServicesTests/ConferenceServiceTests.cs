using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class ConferenceServiceTests
	{
		private readonly Mock<DbSet<Conference>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<Conference> GetTestData()
		{
			return new[]
			{
				TestData.Conference1,
				TestData.Conference2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Conferences).Returns(_mockDbSet.Object);

			TestData.ReportThesis1.UserProfilesReportTheses = new[] {new UserProfilesReportThesis
			{
				ReportThesis = TestData.ReportThesis1,
				UserProfile = TestData.User1
			}};
			TestData.ReportThesis2.UserProfilesReportTheses = new[] {new UserProfilesReportThesis
			{
				ReportThesis = TestData.ReportThesis2,
				UserProfile = TestData.User1
			}};
			TestData.ReportThesis3.UserProfilesReportTheses = new[] {new UserProfilesReportThesis
			{
				ReportThesis = TestData.ReportThesis3,
				UserProfile = TestData.User1
			}};
			
			var userProfileSet = MockProvider.GetMockSet(new []{TestData.User1}.AsQueryable());
			var departmentSet = MockProvider.GetMockSet(new []{TestData.Department1}.AsQueryable());
			var conferenceSet = MockProvider.GetMockSet(new []{TestData.Conference1}.AsQueryable());
			var reportThesesSet = MockProvider.GetMockSet(new []{TestData.ReportThesis1, TestData.ReportThesis2, TestData.ReportThesis3}.AsQueryable());
			
			mockContext.Setup(item => item.UserProfiles).Returns(userProfileSet.Object);
			mockContext.Setup(item => item.Departments).Returns(departmentSet.Object);
			mockContext.Setup(item => item.Conferences).Returns(conferenceSet.Object);
			mockContext.Setup(item => item.ReportTheses).Returns(reportThesesSet.Object);
			
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Conferences).Returns(MockProvider.GetMockSet(list).Object);
			var service = new ConferenceService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ConferenceService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Conference1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ConferenceService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ConferenceService(mockContext.Object);

			var expected = TestData.Conference3;
			service.CreateItem(expected);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ConferenceService(GetMockContext().Object);

			var expected = GetTestData().Last();
			expected.Topic = TestData.Conference3.Topic;
			service.UpdateItem(expected);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ConferenceService(mockContext.Object);

			var item = mockContext.Object.Conferences.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var mockContext = GetMockContext();
			var service = new ConferenceService(mockContext.Object);

			var item = mockContext.Object.Conferences.First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ConferenceService(GetMockContext().Object);

			var item = TestData.Conference3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
		
		[Fact]
		public void GetReportThesesTest()
		{
			var mockContext = GetMockContext();
			var service = new Mock<ConferenceService>(mockContext.Object);

			var conference = mockContext.Object.Conferences.First();

			var actual = service.Object.GetReportTheses(conference.Id);
			
			Assert.NotNull(actual);
			
			service.Verify(it => it.GetReportTheses(conference.Id), Times.Once);
		}

		[Fact]
		public void GetParticipatorsTest()
		{
			var mockContext = GetMockContext();
			var service = new Mock<ConferenceService>(mockContext.Object);

			var conference = mockContext.Object.Conferences.First();

			var actual = service.Object.GetParticipators(conference.Id);

			Assert.NotNull(actual);

			service.Verify(it => it.GetParticipators(conference.Id), Times.Once);
		}
	}
}
