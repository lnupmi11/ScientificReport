using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ReviewRepositoryTests
	{
		private static IEnumerable<Review> GetTestData()
		{
			return new[]
			{
				TestData.Review1,
				TestData.Review2,
				TestData.Review3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.Reviews).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<ReviewRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new ReviewRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.Review1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<ReviewRepository>(GetMockContext().Object);

			var review = GetTestData().First();
			repository.Object.Create(review);

			repository.Setup(item => item.Get(review.Id));
			repository.Object.Get(review.Id);
			repository.Verify(item => item.Get(review.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<ReviewRepository>(GetMockContext().Object);

			var review = GetTestData().First();
			repository.Setup(it => it.Create(review));
			repository.Object.Create(review);
			repository.Verify(it => it.Create(review), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Review>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Reviews).Returns(mockDbSet.Object);

			var repository = new Mock<ReviewRepository>(mockContext.Object);

			var review = GetTestData().First();

			repository.Object.Create(review);

			repository.Setup(a => a.Update(review));
			repository.Object.Update(review);
			repository.Verify(a => a.Update(review));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Review>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Reviews).Returns(mockDbSet.Object);

			var repository = new Mock<ReviewRepository>(mockContext.Object);

			var review = GetTestData().First();

			repository.Setup(x => x.Delete(review.Id));
			repository.Object.Delete(review.Id);
			repository.Verify(i => i.Delete(review.Id));
		}
	}
}
