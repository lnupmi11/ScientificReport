using System;
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
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<UserProfile> GetTestData()
		{
			return new[]
			{
				TestData.User1,
				TestData.User2,
				TestData.User3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.UserProfiles).Returns(MockProvider.GetMockSet(list).Object);
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
			var service = new UserProfileService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.User1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<UserProfileService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<UserProfileService>(_mockContext.Object);

			var expectedUserProfile = TestData.User1;

			service.Setup(it => it.CreateItem(expectedUserProfile));
			service.Object.CreateItem(expectedUserProfile);
			service.Verify(it => it.CreateItem(expectedUserProfile), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<UserProfile>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.UserProfiles).Returns(mockDbSet.Object);

			var service = new UserProfileService(mockContext.Object);

			var userProfile = GetTestData().First();

			service.CreateItem(userProfile);
			service.UpdateItem(userProfile);

			mockDbSet.Verify(m => m.Update(It.IsAny<UserProfile>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<UserProfileService>(_mockContext.Object);

			var userProfile = GetTestData().First();

			service.Setup(x => x.DeleteById(userProfile.Id));
			service.Object.DeleteById(userProfile.Id);
			service.Verify(i => i.DeleteById(userProfile.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<UserProfileService>(_mockContext.Object);

			var userProfile = GetTestData().First();
			service.Object.CreateItem(userProfile);

			service.Setup(a => a.UserExists(userProfile.Id));
			service.Object.UserExists(userProfile.Id);
			service.Verify(a => a.UserExists(userProfile.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<UserProfileService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.UserExists(guid));
			service.Object.UserExists(guid);
			service.Verify(a => a.UserExists(guid));
		}
	}
}
