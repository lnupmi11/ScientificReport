using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class PatentLicenseActivityRepositoryTests
	{
		private static readonly IEnumerable<PatentLicenseActivity> TestPatentLicenseActivities = new[]
		{
			TestData.PatentLicenseActivity1,
			TestData.PatentLicenseActivity2,
			TestData.PatentLicenseActivity3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PatentLicenseActivities).Returns(
				MockProvider.GetMockSet(TestPatentLicenseActivities).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new PatentLicenseActivityRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestPatentLicenseActivities.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new PatentLicenseActivityRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.PatentLicenseActivities.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new PatentLicenseActivityRepository(mockContext.Object);
			var expected = mockContext.Object.PatentLicenseActivities.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PatentLicenseActivityRepository(mockContext.Object);
			Assert.Equal(TestPatentLicenseActivities.Count(), mockContext.Object.PatentLicenseActivities.Count());
			repository.Create(TestData.PatentLicenseActivity1);
			Assert.Equal(TestPatentLicenseActivities.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PatentLicenseActivityRepository(mockContext.Object);
			var item = mockContext.Object.PatentLicenseActivities.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new PatentLicenseActivityRepository(mockContext.Object);
			var item = mockContext.Object.PatentLicenseActivities.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.PatentLicenseActivities.Find(item.Id));
		}
	}
}
