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
	public class GrantRepositoryTests
	{
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
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.Grants).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<GrantRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new GrantRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.Grant1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<GrantRepository>(GetMockContext().Object);

			var grant = GetTestData().First();
			repository.Object.Create(grant);

			repository.Setup(item => item.Get(grant.Id));
			repository.Object.Get(grant.Id);
			repository.Verify(item => item.Get(grant.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<GrantRepository>(GetMockContext().Object);

			var grant = GetTestData().First();
			repository.Setup(it => it.Create(grant));
			repository.Object.Create(grant);
			repository.Verify(it => it.Create(grant), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Grant>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Grants).Returns(mockDbSet.Object);

			var repository = new Mock<GrantRepository>(mockContext.Object);

			var grant = GetTestData().First();

			repository.Object.Create(grant);

			repository.Setup(a => a.Update(grant));
			repository.Object.Update(grant);
			repository.Verify(a => a.Update(grant));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Grant>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Grants).Returns(mockDbSet.Object);

			var repository = new Mock<GrantRepository>(mockContext.Object);

			var grant = GetTestData().First();

			repository.Setup(x => x.Delete(grant.Id));
			repository.Object.Delete(grant.Id);
			repository.Verify(i => i.Delete(grant.Id));
		}
	}
}
