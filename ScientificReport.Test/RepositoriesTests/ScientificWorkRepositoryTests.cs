using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ScientificWorkRepositoryTests
	{
		private static readonly IEnumerable<ScientificWork> TestScientificWorks = new[]
		{
			TestData.ScientificWork1,
			TestData.ScientificWork2,
			TestData.ScientificWork3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificWorks).Returns(
				MockProvider.GetMockSet(TestScientificWorks).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new ScientificWorkRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestScientificWorks.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.ScientificWorks.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			var expected = mockContext.Object.ScientificWorks.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.ScientificWorks.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			var expected = mockContext.Object.ScientificWorks.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			Assert.Equal(TestScientificWorks.Count(), mockContext.Object.ScientificWorks.Count());
			repository.Create(TestData.ScientificWork1);
			Assert.Equal(TestScientificWorks.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			var item = mockContext.Object.ScientificWorks.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificWorkRepository(mockContext.Object);
			var item = mockContext.Object.ScientificWorks.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.ScientificWorks.Find(item.Id));
		}
	}
}
