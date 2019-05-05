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
	public class ArticleRepositoryTests
	{
		private static IEnumerable<Article> GetTestData()
		{
			return new[]
			{
				TestData.Article1,
				TestData.Article2,
				TestData.Article3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Articles).Returns(
				MockProvider.GetMockSet(GetTestData()).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<ArticleRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new ArticleRepository(GetMockContext().Object);

			var actual = repository.AllWhere(a => a.IsPrintCanceled);
			Assert.Equal(2, actual.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<ArticleRepository>(GetMockContext().Object);

			var article = GetTestData().First();
			repository.Object.Create(article);

			repository.Setup(item => item.Get(article.Id));
			repository.Object.Get(article.Id);
			repository.Verify(item => item.Get(article.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<ArticleRepository>(GetMockContext().Object);

			var article = GetTestData().First();
			repository.Setup(it => it.Create(article));
			repository.Object.Create(article);
			repository.Verify(it => it.Create(article), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Article>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Articles).Returns(mockDbSet.Object);

			var repository = new Mock<ArticleRepository>(mockContext.Object);

			var article = GetTestData().First();

			repository.Object.Create(article);

			repository.Setup(a => a.Update(article));
			repository.Object.Update(article);
			repository.Verify(a => a.Update(article));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Article>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Articles).Returns(mockDbSet.Object);

			var repository = new Mock<ArticleRepository>(mockContext.Object);

			var article = GetTestData().First();

			repository.Setup(x => x.Delete(article.Id));
			repository.Object.Delete(article.Id);
			repository.Verify(i => i.Delete(article.Id));
		}
	}
}
