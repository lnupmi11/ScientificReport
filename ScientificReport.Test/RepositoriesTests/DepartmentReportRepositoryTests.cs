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
	public class DepartmentReportRepositoryTests
	{
		private static IEnumerable<DepartmentReport> GetTestData()
		{
			return new[]
			{
				TestData.DepartmentReport1,
				TestData.DepartmentReport2,
				TestData.DepartmentReport3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.DepartmentReports).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<DepartmentReportRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new DepartmentReportRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.DepartmentReport1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<DepartmentReportRepository>(GetMockContext().Object);

			var departmentReport = GetTestData().First();
			repository.Object.Create(departmentReport);

			repository.Setup(item => item.Get(departmentReport.Id));
			repository.Object.Get(departmentReport.Id);
			repository.Verify(item => item.Get(departmentReport.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<DepartmentReportRepository>(GetMockContext().Object);

			var departmentReport = GetTestData().First();
			repository.Setup(it => it.Create(departmentReport));
			repository.Object.Create(departmentReport);
			repository.Verify(it => it.Create(departmentReport), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<DepartmentReport>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.DepartmentReports).Returns(mockDbSet.Object);

			var repository = new Mock<DepartmentReportRepository>(mockContext.Object);

			var departmentReport = GetTestData().First();

			repository.Object.Create(departmentReport);

			repository.Setup(a => a.Update(departmentReport));
			repository.Object.Update(departmentReport);
			repository.Verify(a => a.Update(departmentReport));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<DepartmentReport>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.DepartmentReports).Returns(mockDbSet.Object);

			var repository = new Mock<DepartmentReportRepository>(mockContext.Object);

			var departmentReport = GetTestData().First();

			repository.Setup(x => x.Delete(departmentReport.Id));
			repository.Object.Delete(departmentReport.Id);
			repository.Verify(i => i.Delete(departmentReport.Id));
		}
	}
}
