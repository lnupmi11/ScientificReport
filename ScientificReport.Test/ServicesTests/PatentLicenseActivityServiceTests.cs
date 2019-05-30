using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.PatentLicenseActivity;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class PatentLicenseActivityServiceTests
	{
		private readonly Mock<DbSet<PatentLicenseActivity>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<PatentLicenseActivity> GetTestData()
		{
			TestData.PatentLicenseActivity1.AuthorsPatentLicenseActivities = new List<AuthorsPatentLicenseActivities>
			{
				new AuthorsPatentLicenseActivities
				{
					PatentLicenseActivity = TestData.PatentLicenseActivity1,
					Author = TestData.User1
				}
			};
			TestData.PatentLicenseActivity2.AuthorsPatentLicenseActivities = new List<AuthorsPatentLicenseActivities>
			{
				new AuthorsPatentLicenseActivities
				{
					PatentLicenseActivity = TestData.PatentLicenseActivity2,
					Author = TestData.User1
				}
			};
			return new[]
			{
				TestData.PatentLicenseActivity1,
				TestData.PatentLicenseActivity2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PatentLicenseActivities).Returns(_mockDbSet.Object);
			
			var userProfileSet = MockProvider.GetMockSet(new []{TestData.User1}.AsQueryable());
			var departmentSet = MockProvider.GetMockSet(new []{TestData.Department1}.AsQueryable());
			
			mockContext.Setup(item => item.UserProfiles).Returns(userProfileSet.Object);
			mockContext.Setup(item => item.Departments).Returns(departmentSet.Object);
			
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PatentLicenseActivities).Returns(MockProvider.GetMockSet(list).Object);
			var service = new PatentLicenseActivityService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new PatentLicenseActivityService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.PatentLicenseActivity1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new PatentLicenseActivityService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new PatentLicenseActivityService(GetMockContext().Object);

			var expected = TestData.PatentLicenseActivity3;
			service.CreateItem(new PatentLicenseActivityModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<PatentLicenseActivity>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new PatentLicenseActivityService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Name = TestData.PatentLicenseActivity3.Name;
			service.UpdateItem(new PatentLicenseActivityEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new PatentLicenseActivityService(mockContext.Object);

			var item = mockContext.Object.PatentLicenseActivities.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new PatentLicenseActivityService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new PatentLicenseActivityService(GetMockContext().Object);

			var item = TestData.PatentLicenseActivity3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
		
		[Fact]
		public void GetAuthorsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(GetMockContext().Object);

			service.Setup(item => item.GetAuthors(patentLicenseActivity.Id));
			service.Object.GetAuthors(patentLicenseActivity.Id);
			service.Verify(item => item.GetAuthors(patentLicenseActivity.Id));
		}

		[Fact]
		public void GetApplicantsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(GetMockContext().Object);

			service.Setup(item => item.GetApplicants(patentLicenseActivity.Id));
			service.Object.GetApplicants(patentLicenseActivity.Id);
			service.Verify(item => item.GetApplicants(patentLicenseActivity.Id));
		}

		[Fact]
		public void GetCoauthorsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(GetMockContext().Object);

			service.Setup(item => item.GetCoauthors(patentLicenseActivity.Id));
			service.Object.GetCoauthors(patentLicenseActivity.Id);
			service.Verify(item => item.GetCoauthors(patentLicenseActivity.Id));
		}
		
		[Fact]
		public void GetCoApplicantsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(GetMockContext().Object);

			service.Setup(item => item.GetCoApplicants(patentLicenseActivity.Id));
			service.Object.GetCoApplicants(patentLicenseActivity.Id);
			service.Verify(item => item.GetCoApplicants(patentLicenseActivity.Id));
		}
	}
}
