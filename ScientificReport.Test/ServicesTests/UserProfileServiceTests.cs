using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.UserProfile;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class UserProfileServiceTests
	{
		private readonly Mock<DbSet<UserProfile>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<UserProfile> GetTestData()
		{
			return new[]
			{
				TestData.User1,
				TestData.User2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.UserProfiles).Returns(_mockDbSet.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.UserProfiles).Returns(MockProvider.GetMockSet(list).Object);
			var service = new UserProfileService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new UserProfileService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.User1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new UserProfileService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new UserProfileService(GetMockContext().Object);

			var expected = TestData.User3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<UserProfile>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new UserProfileService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Position = TestData.User3.Position;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new UserProfileService(mockContext.Object);

			var item = mockContext.Object.UserProfiles.First();

			Assert.True(service.UserExists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.UserExists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new UserProfileService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.UserExists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new UserProfileService(GetMockContext().Object);

			var item = TestData.User3;
			var exists = service.UserExists(item.Id);

			Assert.False(exists);
		}
	}
}
