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
	public class MembershipServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<Membership> GetTestData()
		{
			return new[]
			{
				TestData.Membership1,
				TestData.Membership2,
				TestData.Membership3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Memberships).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Memberships).Returns(MockProvider.GetMockSet(list).Object);

			var service = new MembershipService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new MembershipService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Membership1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<MembershipService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<MembershipService>(_mockContext.Object);

			var expectedMembership = TestData.Membership1;

			service.Setup(it => it.CreateItem(expectedMembership));
			service.Object.CreateItem(expectedMembership);
			service.Verify(it => it.CreateItem(expectedMembership), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Membership>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Memberships).Returns(mockDbSet.Object);

			var service = new MembershipService(mockContext.Object);

			var membership = GetTestData().First();

			service.CreateItem(membership);
			service.UpdateItem(membership);

			mockDbSet.Verify(m => m.Update(It.IsAny<Membership>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<MembershipService>(_mockContext.Object);

			var membership = GetTestData().First();

			service.Setup(x => x.DeleteById(membership.Id));
			service.Object.DeleteById(membership.Id);
			service.Verify(i => i.DeleteById(membership.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<MembershipService>(_mockContext.Object);

			var membership = GetTestData().First();
			service.Object.CreateItem(membership);

			service.Setup(a => a.Exists(membership.Id));
			service.Object.Exists(membership.Id);
			service.Verify(a => a.Exists(membership.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<MembershipService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
	}
}
