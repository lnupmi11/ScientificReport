using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class UserProfileRepositoryTests
	{
		private static readonly IEnumerable<UserProfile> TestUserProfiles = new[]
		{
			TestData.User1,
			TestData.User2,
			TestData.User3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.UserProfiles).Returns(
				MockProvider.GetMockSet(TestUserProfiles).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new UserProfileRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestUserProfiles.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new UserProfileRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.UserProfiles.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new UserProfileRepository(mockContext.Object);
			var expected = mockContext.Object.UserProfiles.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new UserProfileRepository(mockContext.Object);
			Assert.Equal(TestUserProfiles.Count(), mockContext.Object.UserProfiles.Count());
			repository.Create(TestData.User1);
			Assert.Equal(TestUserProfiles.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new UserProfileRepository(mockContext.Object);
			var item = mockContext.Object.UserProfiles.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new UserProfileRepository(mockContext.Object);
			var item = mockContext.Object.UserProfiles.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.UserProfiles.Find(item.Id));
		}
	}
}
