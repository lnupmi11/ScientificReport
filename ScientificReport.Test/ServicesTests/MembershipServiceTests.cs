using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Membership;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class MembershipServiceTests
	{
		private readonly Mock<DbSet<Membership>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<Membership> GetTestData()
		{
			return new[]
			{
				TestData.Membership1,
				TestData.Membership2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Memberships).Returns(_mockDbSet.Object);
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
			var service = new MembershipService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Membership1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new MembershipService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new MembershipService(GetMockContext().Object);

			var expected = TestData.Membership3;
			service.CreateItem(new MembershipModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<Membership>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new MembershipService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Type = TestData.Membership3.Type;
			service.UpdateItem(new MembershipEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new MembershipService(mockContext.Object);

			var item = mockContext.Object.Memberships.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new MembershipService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new MembershipService(GetMockContext().Object);

			var item = TestData.Membership3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
	}
}
