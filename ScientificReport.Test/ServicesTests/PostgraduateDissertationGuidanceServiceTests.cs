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
	public class PostgraduateDissertationGuidanceServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<PostgraduateDissertationGuidance> GetTestData()
		{
			return new[]
			{
				TestData.PostgraduateDissertationGuidance1,
				TestData.PostgraduateDissertationGuidance2,
				TestData.PostgraduateDissertationGuidance3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(MockProvider.GetMockSet(list).Object);
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
			var service = new PostgraduateDissertationGuidanceService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.PostgraduateDissertationGuidance1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<PostgraduateDissertationGuidanceService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<PostgraduateDissertationGuidanceService>(_mockContext.Object);

			var expectedPostgraduateDissertationGuidance = TestData.PostgraduateDissertationGuidance1;

			service.Setup(it => it.CreateItem(expectedPostgraduateDissertationGuidance));
			service.Object.CreateItem(expectedPostgraduateDissertationGuidance);
			service.Verify(it => it.CreateItem(expectedPostgraduateDissertationGuidance), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<PostgraduateDissertationGuidance>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(mockDbSet.Object);

			var service = new PostgraduateDissertationGuidanceService(mockContext.Object);

			var postgraduateDissertationGuidance = GetTestData().First();

			service.CreateItem(postgraduateDissertationGuidance);
			service.UpdateItem(postgraduateDissertationGuidance);

			mockDbSet.Verify(m => m.Update(It.IsAny<PostgraduateDissertationGuidance>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<PostgraduateDissertationGuidanceService>(_mockContext.Object);

			var postgraduateDissertationGuidance = GetTestData().First();

			service.Setup(x => x.DeleteById(postgraduateDissertationGuidance.Id));
			service.Object.DeleteById(postgraduateDissertationGuidance.Id);
			service.Verify(i => i.DeleteById(postgraduateDissertationGuidance.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<PostgraduateDissertationGuidanceService>(_mockContext.Object);

			var postgraduateDissertationGuidance = GetTestData().First();
			service.Object.CreateItem(postgraduateDissertationGuidance);

			service.Setup(a => a.Exists(postgraduateDissertationGuidance.Id));
			service.Object.Exists(postgraduateDissertationGuidance.Id);
			service.Verify(a => a.Exists(postgraduateDissertationGuidance.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<PostgraduateDissertationGuidanceService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
	}
}
