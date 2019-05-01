using System;
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
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

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
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Reviews).Returns(MockProvider.GetMockSet(list).Object);
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
			var service = new ReviewService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Review1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<ReviewService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<ReviewService>(_mockContext.Object);

			var expectedReview = TestData.Review1;

			service.Setup(it => it.CreateItem(expectedReview));
			service.Object.CreateItem(expectedReview);
			service.Verify(it => it.CreateItem(expectedReview), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Review>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Reviews).Returns(mockDbSet.Object);

			var service = new ReviewService(mockContext.Object);

			var review = GetTestData().First();

			service.CreateItem(review);
			service.UpdateItem(review);

			mockDbSet.Verify(m => m.Update(It.IsAny<Review>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<ReviewService>(_mockContext.Object);

			var review = GetTestData().First();

			service.Setup(x => x.DeleteById(review.Id));
			service.Object.DeleteById(review.Id);
			service.Verify(i => i.DeleteById(review.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<ReviewService>(_mockContext.Object);

			var review = GetTestData().First();
			service.Object.CreateItem(review);

			service.Setup(a => a.Exists(review.Id));
			service.Object.Exists(review.Id);
			service.Verify(a => a.Exists(review.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<ReviewService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetReviewersTest()
		{
			var review = GetTestData().First();

			var service = new Mock<ReviewService>(_mockContext.Object);

			service.Setup(item => item.GetReviewers(review.Id));
			service.Object.GetReviewers(review.Id);
			service.Verify(item => item.GetReviewers(review.Id));
		}
	}
}
