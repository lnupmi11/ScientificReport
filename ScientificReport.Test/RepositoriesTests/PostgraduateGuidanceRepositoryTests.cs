using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class PostgraduateGuidanceRepositoryTests
	{
		private static readonly IEnumerable<PostgraduateGuidance> TestPostgraduateGuidances = new[]
		{
			TestData.PostgraduateGuidance1,
			TestData.PostgraduateGuidance2,
			TestData.PostgraduateGuidance3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateGuidances).Returns(
				MockProvider.GetMockSet(TestPostgraduateGuidances).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new PostgraduateGuidanceRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestPostgraduateGuidances.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateGuidanceRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.PostgraduateGuidances.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateGuidanceRepository(mockContext.Object);
			var expected = mockContext.Object.PostgraduateGuidances.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateGuidanceRepository(mockContext.Object);
			Assert.Equal(TestPostgraduateGuidances.Count(), mockContext.Object.PostgraduateGuidances.Count());
			repository.Create(TestData.PostgraduateGuidance1);
			Assert.Equal(TestPostgraduateGuidances.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateGuidanceRepository(mockContext.Object);
			var item = mockContext.Object.PostgraduateGuidances.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateGuidanceRepository(mockContext.Object);
			var item = mockContext.Object.PostgraduateGuidances.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.PostgraduateGuidances.Find(item.Id));
		}
	}
}
