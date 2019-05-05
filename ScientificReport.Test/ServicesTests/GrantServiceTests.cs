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
	public class GrantServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<Grant> GetTestData()
		{
			return new[]
			{
				TestData.Grant1,
				TestData.Grant2,
				TestData.Grant3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Grants).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Grants).Returns(MockProvider.GetMockSet(list).Object);

			var service = new GrantService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new GrantService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Grant1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<GrantService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<GrantService>(_mockContext.Object);

			var expectedGrant = TestData.Grant1;

			service.Setup(it => it.CreateItem(expectedGrant));
			service.Object.CreateItem(expectedGrant);
			service.Verify(it => it.CreateItem(expectedGrant), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Grant>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Grants).Returns(mockDbSet.Object);

			var service = new GrantService(mockContext.Object);

			var grant = GetTestData().First();

			service.CreateItem(grant);
			service.UpdateItem(grant);

			mockDbSet.Verify(m => m.Update(It.IsAny<Grant>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<GrantService>(_mockContext.Object);

			var grant = GetTestData().First();

			service.Setup(x => x.DeleteById(grant.Id));
			service.Object.DeleteById(grant.Id);
			service.Verify(i => i.DeleteById(grant.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<GrantService>(_mockContext.Object);

			var grant = GetTestData().First();
			service.Object.CreateItem(grant);

			service.Setup(a => a.Exists(grant.Id));
			service.Object.Exists(grant.Id);
			service.Verify(a => a.Exists(grant.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<GrantService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetUsersTest()
		{
			var service = new Mock<GrantService>(_mockContext.Object);

			service.Setup(item => item.GetUsers(TestData.Grant1.Id));
			service.Object.GetUsers(TestData.Grant1.Id);
			service.Verify(item => item.GetUsers(TestData.Grant1.Id));
		}
	}
}
