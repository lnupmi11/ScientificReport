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
	public class PostgraduateGuidanceServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<PostgraduateGuidance> GetTestData()
		{
			return new[]
			{
				TestData.PostgraduateGuidance1,
				TestData.PostgraduateGuidance2,
				TestData.PostgraduateGuidance3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateGuidances).Returns(MockProvider.GetMockSet(list).Object);
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
			var service = new PostgraduateGuidanceService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.PostgraduateGuidance1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<PostgraduateGuidanceService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<PostgraduateGuidanceService>(_mockContext.Object);

			var expectedPostgraduateGuidance = TestData.PostgraduateGuidance1;

			service.Setup(it => it.CreateItem(expectedPostgraduateGuidance));
			service.Object.CreateItem(expectedPostgraduateGuidance);
			service.Verify(it => it.CreateItem(expectedPostgraduateGuidance), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<PostgraduateGuidance>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PostgraduateGuidances).Returns(mockDbSet.Object);

			var service = new PostgraduateGuidanceService(mockContext.Object);

			var postgraduateGuidance = GetTestData().First();

			service.CreateItem(postgraduateGuidance);
			service.UpdateItem(postgraduateGuidance);

			mockDbSet.Verify(m => m.Update(It.IsAny<PostgraduateGuidance>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<PostgraduateGuidanceService>(_mockContext.Object);

			var postgraduateGuidance = GetTestData().First();

			service.Setup(x => x.DeleteById(postgraduateGuidance.Id));
			service.Object.DeleteById(postgraduateGuidance.Id);
			service.Verify(i => i.DeleteById(postgraduateGuidance.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<PostgraduateGuidanceService>(_mockContext.Object);

			var postgraduateGuidance = GetTestData().First();
			service.Object.CreateItem(postgraduateGuidance);

			service.Setup(a => a.Exists(postgraduateGuidance.Id));
			service.Object.Exists(postgraduateGuidance.Id);
			service.Verify(a => a.Exists(postgraduateGuidance.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<PostgraduateGuidanceService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
	}
}
