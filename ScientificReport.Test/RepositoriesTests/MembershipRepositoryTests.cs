using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class MembershipRepositoryTests
	{
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
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.Memberships).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<MembershipRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new MembershipRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.Membership1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<MembershipRepository>(GetMockContext().Object);

			var membership = GetTestData().First();
			repository.Object.Create(membership);

			repository.Setup(item => item.Get(membership.Id));
			repository.Object.Get(membership.Id);
			repository.Verify(item => item.Get(membership.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<MembershipRepository>(GetMockContext().Object);

			var membership = GetTestData().First();
			repository.Setup(it => it.Create(membership));
			repository.Object.Create(membership);
			repository.Verify(it => it.Create(membership), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Membership>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Memberships).Returns(mockDbSet.Object);

			var repository = new Mock<MembershipRepository>(mockContext.Object);

			var membership = GetTestData().First();

			repository.Object.Create(membership);

			repository.Setup(a => a.Update(membership));
			repository.Object.Update(membership);
			repository.Verify(a => a.Update(membership));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Membership>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Memberships).Returns(mockDbSet.Object);

			var repository = new Mock<MembershipRepository>(mockContext.Object);

			var membership = GetTestData().First();

			repository.Setup(x => x.Delete(membership.Id));
			repository.Object.Delete(membership.Id);
			repository.Verify(i => i.Delete(membership.Id));
		}
	}
}
