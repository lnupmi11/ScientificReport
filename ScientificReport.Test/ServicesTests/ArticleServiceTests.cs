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
		private readonly Mock<DbSet<Article>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());
		
		private static IEnumerable<Article> GetTestData()
		{
			return new[]
			{
				TestData.Article1,
				TestData.Article2
			};
		}
		
		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Articles).Returns(_mockDbSet.Object);
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
			var service = new ArticleService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Article1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ArticleService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);
			
			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new ArticleService(GetMockContext().Object);

			var expected = TestData.Article3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<Article>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ArticleService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Title = TestData.Article3.Title;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ArticleService(mockContext.Object);
	
			var item = mockContext.Object.Articles.First();
			
			Assert.True(service.Exists(item.Id));
			
			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new ArticleService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);
			
			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ArticleService(GetMockContext().Object);

			var item = TestData.Article3;
			var exists = service.Exists(item.Id);
			
			Assert.False(exists);
		}

		[Fact]
		public void GetAuthorsTest()
		{
			var service = new Mock<ArticleService>(GetMockContext().Object);

			var article = _mockDbSet.Object.FirstOrDefault();
			
			Assert.NotNull(article);

			var actual = service.Object.GetAuthors(article.Id);
			
			Assert.NotNull(actual);
			
			service.Verify(m => m.GetAuthors(article.Id), Times.Once);
		}
	}
}
