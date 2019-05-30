using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ReviewRepositoryTests
	{
		private static readonly IEnumerable<Review> TestReviews = new[]
		{
			TestData.Review1,
			TestData.Review2,
			TestData.Review3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Reviews).Returns(
				MockProvider.GetMockSet(TestReviews).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new ReviewRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestReviews.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Reviews.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			var expected = mockContext.Object.Reviews.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.Reviews.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			var expected = mockContext.Object.Reviews.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			Assert.Equal(TestReviews.Count(), mockContext.Object.Reviews.Count());
			repository.Create(TestData.Review1);
			Assert.Equal(TestReviews.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			var item = mockContext.Object.Reviews.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new ReviewRepository(mockContext.Object);
			var item = mockContext.Object.Reviews.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Reviews.Find(item.Id));
		}
	}
}
