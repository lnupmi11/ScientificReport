using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class TeacherReportRepositoryTests
	{
		private static readonly IEnumerable<TeacherReport> TestTeacherReports = new[]
		{
			TestData.TeacherReport1,
			TestData.TeacherReport2,
			TestData.TeacherReport3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.TeacherReports).Returns(
				MockProvider.GetMockSet(TestTeacherReports).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new TeacherReportRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestTeacherReports.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.TeacherReports.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			var expected = mockContext.Object.TeacherReports.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.TeacherReports.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			var expected = mockContext.Object.TeacherReports.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			Assert.Equal(TestTeacherReports.Count(), mockContext.Object.TeacherReports.Count());
			repository.Create(TestData.TeacherReport1);
			Assert.Equal(TestTeacherReports.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			var item = mockContext.Object.TeacherReports.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new TeacherReportRepository(mockContext.Object);
			var item = mockContext.Object.TeacherReports.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.TeacherReports.Find(item.Id));
		}
	}
}
