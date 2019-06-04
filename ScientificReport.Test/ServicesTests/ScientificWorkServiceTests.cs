using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Publications;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class ScientificWorkServiceTests
	{
		private readonly Mock<DbSet<ScientificWork>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<ScientificWork> GetTestData()
		{
			return new[]
			{
				TestData.ScientificWork1,
				TestData.ScientificWork2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificWorks).Returns(_mockDbSet.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificWorks).Returns(MockProvider.GetMockSet(list).Object);
			var service = new ScientificWorkService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ScientificWorkService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ScientificWork1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ScientificWorkService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new ScientificWorkService(GetMockContext().Object);

			var expected = TestData.ScientificWork3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<ScientificWork>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ScientificWorkService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Title = TestData.ScientificWork3.Title;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ScientificWorkService(mockContext.Object);

			var item = mockContext.Object.ScientificWorks.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new ScientificWorkService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ScientificWorkService(GetMockContext().Object);

			var item = TestData.ScientificWork3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}

		[Fact]
		public void GetAuthorsTest()
		{
			var scientificWork = GetTestData().First();

			var service = new Mock<ScientificWorkService>(GetMockContext().Object);

			service.Setup(item => item.GetAuthors(scientificWork.Id));
			service.Object.GetAuthors(scientificWork.Id);
			service.Verify(item => item.GetAuthors(scientificWork.Id));
		}
		
		[Fact]
		public void AddAuthorTest()
		{
			var scientificWork = GetTestData().First();

			var service = new Mock<ScientificWorkService>(GetMockContext().Object);

			service.Setup(item => item.AddAuthor(scientificWork.Id, TestData.User3.Id));
			service.Object.AddAuthor(scientificWork.Id, TestData.User3.Id);
			service.Verify(item => item.AddAuthor(scientificWork.Id, TestData.User3.Id));
		}

		[Fact]
		public void RemoveAuthorTest()
		{
			var scientificWork = GetTestData().First();

			var service = new Mock<ScientificWorkService>(GetMockContext().Object);

			service.Object.AddAuthor(scientificWork.Id, TestData.User3.Id);
			
			service.Setup(item => item.RemoveAuthor(scientificWork.Id, TestData.User3.Id));
			service.Object.RemoveAuthor(scientificWork.Id, TestData.User3.Id);
			service.Verify(item => item.RemoveAuthor(scientificWork.Id, TestData.User3.Id));
		}
	}
}
