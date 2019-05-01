using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class PatentLicenseActivityRepositoryTests
	{
		private static IEnumerable<PatentLicenseActivity> GetTestData()
		{
			return new[]
			{
				TestData.PatentLicenseActivity1,
				TestData.PatentLicenseActivity2,
				TestData.PatentLicenseActivity3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.PatentLicenseActivities).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<PatentLicenseActivityRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new PatentLicenseActivityRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.PatentLicenseActivity1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<PatentLicenseActivityRepository>(GetMockContext().Object);

			var patentLicenseActivity = GetTestData().First();
			repository.Object.Create(patentLicenseActivity);

			repository.Setup(item => item.Get(patentLicenseActivity.Id));
			repository.Object.Get(patentLicenseActivity.Id);
			repository.Verify(item => item.Get(patentLicenseActivity.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<PatentLicenseActivityRepository>(GetMockContext().Object);

			var patentLicenseActivity = GetTestData().First();
			repository.Setup(it => it.Create(patentLicenseActivity));
			repository.Object.Create(patentLicenseActivity);
			repository.Verify(it => it.Create(patentLicenseActivity), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<PatentLicenseActivity>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PatentLicenseActivities).Returns(mockDbSet.Object);

			var repository = new Mock<PatentLicenseActivityRepository>(mockContext.Object);

			var patentLicenseActivity = GetTestData().First();

			repository.Object.Create(patentLicenseActivity);

			repository.Setup(a => a.Update(patentLicenseActivity));
			repository.Object.Update(patentLicenseActivity);
			repository.Verify(a => a.Update(patentLicenseActivity));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<PatentLicenseActivity>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PatentLicenseActivities).Returns(mockDbSet.Object);

			var repository = new Mock<PatentLicenseActivityRepository>(mockContext.Object);

			var patentLicenseActivity = GetTestData().First();

			repository.Setup(x => x.Delete(patentLicenseActivity.Id));
			repository.Object.Delete(patentLicenseActivity.Id);
			repository.Verify(i => i.Delete(patentLicenseActivity.Id));
		}
	}
}
