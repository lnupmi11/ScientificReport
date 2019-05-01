using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class ArticleServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();
		
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
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Articles).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Articles).Returns(MockProvider.GetMockSet(list).Object);

			var service = new ArticleService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ArticleService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Article1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<ArticleService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<ArticleService>(_mockContext.Object);

			var expected = TestData.Article1;

			service.Setup(it => it.CreateItem(expected));
			service.Object.CreateItem(expected);
			service.Verify(it => it.CreateItem(expected), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Article>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Articles).Returns(mockDbSet.Object);

			var service = new ArticleService(mockContext.Object);

			var article = GetTestData().First();

			service.CreateItem(article);
			service.UpdateItem(article);

			mockDbSet.Verify(m => m.Update(It.IsAny<Article>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<ArticleService>(_mockContext.Object);

			var article = GetTestData().First();

			service.Setup(x => x.DeleteById(article.Id));
			service.Object.DeleteById(article.Id);
			service.Verify(i => i.DeleteById(article.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<ArticleService>(_mockContext.Object);

			var article = GetTestData().First();
			service.Object.CreateItem(article);

			service.Setup(a => a.Exists(article.Id));
			service.Object.Exists(article.Id);
			service.Verify(a => a.Exists(article.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<ArticleService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetAuthorsTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Articles).Returns(MockProvider.GetMockSet(list).Object);

			var article = GetTestData().First();

			var service = new Mock<ArticleService>(mockContext.Object);

			service.Object.CreateItem(article);

			service.Setup(item => item.GetAuthors(article.Id));
			service.Object.GetAuthors(article.Id);
			service.Verify(item => item.GetAuthors(article.Id));
		}
	}
}
