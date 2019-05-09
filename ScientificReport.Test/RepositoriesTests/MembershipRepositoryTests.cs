using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class MembershipRepositoryTests
	{
		private static readonly IEnumerable<Membership> TestMemberships = new[]
		{
			TestData.Membership1,
			TestData.Membership2,
			TestData.Membership3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Memberships).Returns(
				MockProvider.GetMockSet(TestMemberships).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new MembershipRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestMemberships.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new MembershipRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Memberships.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new MembershipRepository(mockContext.Object);
			var expected = mockContext.Object.Memberships.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new MembershipRepository(mockContext.Object);
			Assert.Equal(TestMemberships.Count(), mockContext.Object.Memberships.Count());
			repository.Create(TestData.Membership1);
			Assert.Equal(TestMemberships.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new MembershipRepository(mockContext.Object);
			var item = mockContext.Object.Memberships.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new MembershipRepository(mockContext.Object);
			var item = mockContext.Object.Memberships.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Memberships.Find(item.Id));
		}
	}
}
