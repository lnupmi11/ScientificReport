using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ScientificInternshipRepositoryTests
	{
		private static readonly IEnumerable<ScientificInternship> TestScientificInternships = new[]
		{
			TestData.ScientificInternship1,
			TestData.ScientificInternship2,
			TestData.ScientificInternship3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificInternships).Returns(
				MockProvider.GetMockSet(TestScientificInternships).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new ScientificInternshipRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestScientificInternships.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.ScientificInternships.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			var expected = mockContext.Object.ScientificInternships.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.ScientificInternships.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			var expected = mockContext.Object.ScientificInternships.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			Assert.Equal(TestScientificInternships.Count(), mockContext.Object.ScientificInternships.Count());
			repository.Create(TestData.ScientificInternship1);
			Assert.Equal(TestScientificInternships.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			var item = mockContext.Object.ScientificInternships.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificInternshipRepository(mockContext.Object);
			var item = mockContext.Object.ScientificInternships.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.ScientificInternships.Find(item.Id));
		}
	}
}
