using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class PostgraduateDissertationGuidanceRepositoryTests
	{
		private static readonly IEnumerable<PostgraduateDissertationGuidance> TestPostgraduateDissertationGuidances = new[]
		{
			TestData.PostgraduateDissertationGuidance1,
			TestData.PostgraduateDissertationGuidance2,
			TestData.PostgraduateDissertationGuidance3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(
				MockProvider.GetMockSet(TestPostgraduateDissertationGuidances).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new PostgraduateDissertationGuidanceRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestPostgraduateDissertationGuidances.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.PostgraduateDissertationGuidances.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			var expected = mockContext.Object.PostgraduateDissertationGuidances.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.PostgraduateDissertationGuidances.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			var expected = mockContext.Object.PostgraduateDissertationGuidances.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			Assert.Equal(TestPostgraduateDissertationGuidances.Count(), mockContext.Object.PostgraduateDissertationGuidances.Count());
			repository.Create(TestData.PostgraduateDissertationGuidance1);
			Assert.Equal(TestPostgraduateDissertationGuidances.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			var item = mockContext.Object.PostgraduateDissertationGuidances.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new PostgraduateDissertationGuidanceRepository(mockContext.Object);
			var item = mockContext.Object.PostgraduateDissertationGuidances.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.PostgraduateDissertationGuidances.Find(item.Id));
		}
	}
}
