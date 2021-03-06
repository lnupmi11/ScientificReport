using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.PostgraduateGuidance;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class PostgraduateGuidanceServiceTests
	{
		private readonly Mock<DbSet<PostgraduateGuidance>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<PostgraduateGuidance> GetTestData()
		{
			return new[]
			{
				TestData.PostgraduateGuidance1,
				TestData.PostgraduateGuidance2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateGuidances).Returns(_mockDbSet.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateGuidances).Returns(MockProvider.GetMockSet(list).Object);
			var service = new PostgraduateGuidanceService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new PostgraduateGuidanceService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.PostgraduateGuidance1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new PostgraduateGuidanceService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new PostgraduateGuidanceService(GetMockContext().Object);

			var expected = TestData.PostgraduateGuidance3;
			service.CreateItem(new PostgraduateGuidanceModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<PostgraduateGuidance>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new PostgraduateGuidanceService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.PostgraduateInfo = TestData.PostgraduateGuidance3.PostgraduateInfo;
			service.UpdateItem(new PostgraduateGuidanceEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new PostgraduateGuidanceService(mockContext.Object);

			var item = mockContext.Object.PostgraduateGuidances.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new PostgraduateGuidanceService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new PostgraduateGuidanceService(GetMockContext().Object);

			var item = TestData.PostgraduateGuidance3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
	}
}
