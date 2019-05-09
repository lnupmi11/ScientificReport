using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class OppositionRepositoryTests
	{
		private static readonly IEnumerable<Opposition> TestOppositions = new[]
		{
			TestData.Opposition1,
			TestData.Opposition2,
			TestData.Opposition3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Oppositions).Returns(
				MockProvider.GetMockSet(TestOppositions).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new OppositionRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestOppositions.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new OppositionRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Oppositions.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new OppositionRepository(mockContext.Object);
			var expected = mockContext.Object.Oppositions.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new OppositionRepository(mockContext.Object);
			Assert.Equal(TestOppositions.Count(), mockContext.Object.Oppositions.Count());
			repository.Create(TestData.Opposition1);
			Assert.Equal(TestOppositions.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new OppositionRepository(mockContext.Object);
			var item = mockContext.Object.Oppositions.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new OppositionRepository(mockContext.Object);
			var item = mockContext.Object.Oppositions.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Oppositions.Find(item.Id));
		}
	}
}
