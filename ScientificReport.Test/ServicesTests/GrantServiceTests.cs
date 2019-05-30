using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DTO.Models.Grant;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class GrantServiceTests
	{
		private readonly Mock<DbSet<Grant>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<Grant> GetTestData()
		{
			TestData.Grant1.UserProfilesGrants = new List<UserProfilesGrants>
			{
				new UserProfilesGrants
				{
					UserProfile = TestData.User1,
					Grant = TestData.Grant1,
				}
			};
			TestData.Grant2.UserProfilesGrants = new List<UserProfilesGrants>
			{
				new UserProfilesGrants
				{
					UserProfile = TestData.User1,
					Grant = TestData.Grant2,
				}
			};
			return new[]
			{
				TestData.Grant1,
				TestData.Grant2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Grants).Returns(_mockDbSet.Object);
			
			var userProfileSet = MockProvider.GetMockSet(new []{TestData.User1}.AsQueryable());
			var departmentSet = MockProvider.GetMockSet(new []{TestData.Department1}.AsQueryable());
			
			mockContext.Setup(item => item.UserProfiles).Returns(userProfileSet.Object);
			mockContext.Setup(item => item.Departments).Returns(departmentSet.Object);
			
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
			var service = new GrantService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Grant1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new GrantService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new GrantService(GetMockContext().Object);

			var expected = TestData.Grant3;
			service.CreateItem(new GrantModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<Grant>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new GrantService(GetMockContext().Object);

			var expected = GetTestData().First();
			service.UpdateItem(new GrantEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new GrantService(mockContext.Object);

			var item = mockContext.Object.Grants.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new GrantService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new GrantService(GetMockContext().Object);

			var item = TestData.Grant3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
	}
}
