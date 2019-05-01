using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class UserProfileRepositoryTests
	{
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
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.UserProfiles).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<UserProfileRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new UserProfileRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.User1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<UserProfileRepository>(GetMockContext().Object);

			var userProfile = GetTestData().First();
			repository.Object.Create(userProfile);

			repository.Setup(item => item.Get(userProfile.Id));
			repository.Object.Get(userProfile.Id);
			repository.Verify(item => item.Get(userProfile.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<UserProfileRepository>(GetMockContext().Object);

			var userProfile = GetTestData().First();
			repository.Setup(it => it.Create(userProfile));
			repository.Object.Create(userProfile);
			repository.Verify(it => it.Create(userProfile), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<UserProfile>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.UserProfiles).Returns(mockDbSet.Object);

			var repository = new Mock<UserProfileRepository>(mockContext.Object);

			var userProfile = GetTestData().First();

			repository.Object.Create(userProfile);

			repository.Setup(a => a.Update(userProfile));
			repository.Object.Update(userProfile);
			repository.Verify(a => a.Update(userProfile));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<UserProfile>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.UserProfiles).Returns(mockDbSet.Object);

			var repository = new Mock<UserProfileRepository>(mockContext.Object);

			var userProfile = GetTestData().First();

			repository.Setup(x => x.Delete(userProfile.Id));
			repository.Object.Delete(userProfile.Id);
			repository.Verify(i => i.Delete(userProfile.Id));
		}
	}
}
