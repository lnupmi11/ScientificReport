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
	public class OppositionServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<Opposition> GetTestData()
		{
			return new[]
			{
				TestData.Opposition1,
				TestData.Opposition2,
				TestData.Opposition3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Oppositions).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Oppositions).Returns(MockProvider.GetMockSet(list).Object);

			var service = new OppositionService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new OppositionService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Opposition1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<OppositionService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<OppositionService>(_mockContext.Object);

			var expectedOpposition = TestData.Opposition1;

			service.Setup(it => it.CreateItem(expectedOpposition));
			service.Object.CreateItem(expectedOpposition);
			service.Verify(it => it.CreateItem(expectedOpposition), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Opposition>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Oppositions).Returns(mockDbSet.Object);

			var service = new OppositionService(mockContext.Object);

			var opposition = GetTestData().First();

			service.CreateItem(opposition);
			service.UpdateItem(opposition);

			mockDbSet.Verify(m => m.Update(It.IsAny<Opposition>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<OppositionService>(_mockContext.Object);

			var opposition = GetTestData().First();

			service.Setup(x => x.DeleteById(opposition.Id));
			service.Object.DeleteById(opposition.Id);
			service.Verify(i => i.DeleteById(opposition.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<OppositionService>(_mockContext.Object);

			var opposition = GetTestData().First();
			service.Object.CreateItem(opposition);

			service.Setup(a => a.Exists(opposition.Id));
			service.Object.Exists(opposition.Id);
			service.Verify(a => a.Exists(opposition.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<OppositionService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
	}
}
