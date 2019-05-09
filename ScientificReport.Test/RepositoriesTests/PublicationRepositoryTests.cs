using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class PublicationRepositoryTests
	{
		private static readonly IEnumerable<Publication> TestPublications = new[]
		{
			TestData.Publication1,
			TestData.Publication2,
			TestData.Publication3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(
				MockProvider.GetMockSet(TestPublications).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new PublicationRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestPublications.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new PublicationRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Publications.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new PublicationRepository(mockContext.Object);
			var expected = mockContext.Object.Publications.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PublicationRepository(mockContext.Object);
			Assert.Equal(TestPublications.Count(), mockContext.Object.Publications.Count());
			repository.Create(TestData.Publication1);
			Assert.Equal(TestPublications.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PublicationRepository(mockContext.Object);
			var item = mockContext.Object.Publications.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new PublicationRepository(mockContext.Object);
			var item = mockContext.Object.Publications.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Publications.Find(item.Id));
		}
	}
}
