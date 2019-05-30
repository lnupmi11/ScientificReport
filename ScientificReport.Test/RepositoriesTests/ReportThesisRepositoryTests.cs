using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ReportThesisRepositoryTests
	{
		private static readonly IEnumerable<ReportThesis> TestReportTheses = new[]
		{
			TestData.ReportThesis1,
			TestData.ReportThesis2,
			TestData.ReportThesis3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ReportTheses).Returns(
				MockProvider.GetMockSet(TestReportTheses).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new ReportThesisRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestReportTheses.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.ReportTheses.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			var expected = mockContext.Object.ReportTheses.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.ReportTheses.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			var expected = mockContext.Object.ReportTheses.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			Assert.Equal(TestReportTheses.Count(), mockContext.Object.ReportTheses.Count());
			repository.Create(TestData.ReportThesis1);
			Assert.Equal(TestReportTheses.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			var item = mockContext.Object.ReportTheses.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReportThesisRepository(mockContext.Object);
			var item = mockContext.Object.ReportTheses.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.ReportTheses.Find(item.Id));
		}
	}
}
