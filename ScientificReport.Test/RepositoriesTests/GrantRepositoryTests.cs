using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class GrantRepositoryTests
	{
		private static readonly IEnumerable<Grant> TestGrants = new[]
		{
			TestData.Grant1,
			TestData.Grant2,
			TestData.Grant3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Grants).Returns(
				MockProvider.GetMockSet(TestGrants).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new GrantRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestGrants.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new GrantRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Grants.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new GrantRepository(mockContext.Object);
			var expected = mockContext.Object.Grants.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new GrantRepository(mockContext.Object);
			Assert.Equal(TestGrants.Count(), mockContext.Object.Grants.Count());
			repository.Create(TestData.Grant1);
			Assert.Equal(TestGrants.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new GrantRepository(mockContext.Object);
			var item = mockContext.Object.Grants.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new GrantRepository(mockContext.Object);
			var item = mockContext.Object.Grants.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Grants.Find(item.Id));
		}
	}
}
