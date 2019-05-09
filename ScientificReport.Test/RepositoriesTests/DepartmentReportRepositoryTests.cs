using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class DepartmentReportRepositoryTests
	{
		private static readonly IEnumerable<DepartmentReport> TestDepartmentReports = new[]
		{
			TestData.DepartmentReport1,
			TestData.DepartmentReport2,
			TestData.DepartmentReport3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.DepartmentReports).Returns(
				MockProvider.GetMockSet(TestDepartmentReports).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new DepartmentReportRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestDepartmentReports.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentReportRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.DepartmentReports.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentReportRepository(mockContext.Object);
			var expected = mockContext.Object.DepartmentReports.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentReportRepository(mockContext.Object);
			Assert.Equal(TestDepartmentReports.Count(), mockContext.Object.DepartmentReports.Count());
			repository.Create(TestData.DepartmentReport1);
			Assert.Equal(TestDepartmentReports.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentReportRepository(mockContext.Object);
			var item = mockContext.Object.DepartmentReports.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentReportRepository(mockContext.Object);
			var item = mockContext.Object.DepartmentReports.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.DepartmentReports.Find(item.Id));
		}
	}
}
