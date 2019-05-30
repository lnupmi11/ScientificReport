using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ArticleRepositoryTests
	{
		private static readonly IEnumerable<Article> TestArticles = new[]
		{
			TestData.Article1,
			TestData.Article2,
			TestData.Article3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Articles).Returns(
				MockProvider.GetMockSet(TestArticles).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new ArticleRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestArticles.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Articles.First().Id);
			Assert.Single(actual);
		}

		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			var expected = mockContext.Object.Articles.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.Articles.Count());
		}
		
		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			var expected = mockContext.Object.Articles.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			Assert.Equal(TestArticles.Count(), mockContext.Object.Articles.Count());
			repository.Create(TestData.Article1);
			Assert.Equal(TestArticles.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			var item = mockContext.Object.Articles.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new ArticleRepository(mockContext.Object);
			var item = mockContext.Object.Articles.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Articles.Find(item.Id));
		}
	}
}
