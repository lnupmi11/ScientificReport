using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.PostgraduateDissertationGuidance;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class PostgraduateDissertationGuidanceServiceTests
	{
		private readonly Mock<DbSet<PostgraduateDissertationGuidance>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<PostgraduateDissertationGuidance> GetTestData()
		{
			return new[]
			{
				TestData.PostgraduateDissertationGuidance1,
				TestData.PostgraduateDissertationGuidance2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(_mockDbSet.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(MockProvider.GetMockSet(list).Object);
			var service = new PostgraduateDissertationGuidanceService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new PostgraduateDissertationGuidanceService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.PostgraduateDissertationGuidance1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new PostgraduateDissertationGuidanceService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new PostgraduateDissertationGuidanceService(GetMockContext().Object);

			var expected = TestData.PostgraduateDissertationGuidance3;
			service.CreateItem(new PostgraduateDissertationGuidanceModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<PostgraduateDissertationGuidance>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new PostgraduateDissertationGuidanceService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Speciality = TestData.PostgraduateDissertationGuidance3.Speciality;
			service.UpdateItem(new PostgraduateDissertationGuidanceEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new PostgraduateDissertationGuidanceService(mockContext.Object);

			var item = mockContext.Object.PostgraduateDissertationGuidances.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new PostgraduateDissertationGuidanceService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new PostgraduateDissertationGuidanceService(GetMockContext().Object);

			var item = TestData.PostgraduateDissertationGuidance3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
	}
}
