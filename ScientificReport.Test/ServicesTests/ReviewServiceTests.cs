using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class ReviewServiceTests
	{
		private readonly Mock<DbSet<Review>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<Review> GetTestData()
		{
			return new[]
			{
				TestData.Review1,
				TestData.Review2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Reviews).Returns(_mockDbSet.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Reviews).Returns(MockProvider.GetMockSet(list).Object);
			var service = new ReviewService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ReviewService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Review1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ReviewService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new ReviewService(GetMockContext().Object);

			var expected = TestData.Review3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<Review>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ReviewService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.DateOfReview = TestData.Review3.DateOfReview;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ReviewService(mockContext.Object);

			var item = mockContext.Object.Reviews.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new ReviewService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ReviewService(GetMockContext().Object);

			var item = TestData.Review3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
		
		[Fact]
		public void GetReviewersTest()
		{
			var review = GetTestData().First();

			var service = new Mock<ReviewService>(GetMockContext().Object);

			service.Setup(item => item.GetReviewers(review.Id));
			service.Object.GetReviewers(review.Id);
			service.Verify(item => item.GetReviewers(review.Id));
		}
	}
}
