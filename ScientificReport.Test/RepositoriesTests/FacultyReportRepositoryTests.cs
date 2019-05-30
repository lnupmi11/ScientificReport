using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class FacultyReportRepositoryTests
	{
		private static readonly IEnumerable<FacultyReport> TestFacultyReports = new[]
		{
			TestData.FacultyReport1,
			TestData.FacultyReport2,
			TestData.FacultyReport3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.FacultyReports).Returns(
				MockProvider.GetMockSet(TestFacultyReports).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new FacultyReportRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestFacultyReports.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.FacultyReports.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			var expected = mockContext.Object.FacultyReports.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.FacultyReports.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			var expected = mockContext.Object.FacultyReports.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			Assert.Equal(TestFacultyReports.Count(), mockContext.Object.FacultyReports.Count());
			repository.Create(TestData.FacultyReport1);
			Assert.Equal(TestFacultyReports.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			var item = mockContext.Object.FacultyReports.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new FacultyReportRepository(mockContext.Object);
			var item = mockContext.Object.FacultyReports.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.FacultyReports.Find(item.Id));
		}
	}
}
