using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class FacultyReportRepositoryTests
	{
		private static IEnumerable<FacultyReport> GetTestData()
		{
			return new[]
			{
				TestData.FacultyReport1,
				TestData.FacultyReport2,
				TestData.FacultyReport3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.FacultyReports).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<FacultyReportRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new FacultyReportRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.FacultyReport1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<FacultyReportRepository>(GetMockContext().Object);

			var facultyReport = GetTestData().First();
			repository.Object.Create(facultyReport);

			repository.Setup(item => item.Get(facultyReport.Id));
			repository.Object.Get(facultyReport.Id);
			repository.Verify(item => item.Get(facultyReport.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<FacultyReportRepository>(GetMockContext().Object);

			var facultyReport = GetTestData().First();
			repository.Setup(it => it.Create(facultyReport));
			repository.Object.Create(facultyReport);
			repository.Verify(it => it.Create(facultyReport), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<FacultyReport>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.FacultyReports).Returns(mockDbSet.Object);

			var repository = new Mock<FacultyReportRepository>(mockContext.Object);

			var facultyReport = GetTestData().First();

			repository.Object.Create(facultyReport);

			repository.Setup(a => a.Update(facultyReport));
			repository.Object.Update(facultyReport);
			repository.Verify(a => a.Update(facultyReport));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<FacultyReport>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.FacultyReports).Returns(mockDbSet.Object);

			var repository = new Mock<FacultyReportRepository>(mockContext.Object);

			var facultyReport = GetTestData().First();

			repository.Setup(x => x.Delete(facultyReport.Id));
			repository.Object.Delete(facultyReport.Id);
			repository.Verify(i => i.Delete(facultyReport.Id));
		}
	}
}
