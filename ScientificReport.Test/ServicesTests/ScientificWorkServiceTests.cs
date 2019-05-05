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
	public class ScientificWorkServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<ScientificWork> GetTestData()
		{
			return new[]
			{
				TestData.ScientificWork1,
				TestData.ScientificWork2,
				TestData.ScientificWork3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificWorks).Returns(MockProvider.GetMockSet(list).Object);
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
			var service = new ScientificWorkService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ScientificWork1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			var expectedScientificWork = TestData.ScientificWork1;

			service.Setup(it => it.CreateItem(expectedScientificWork));
			service.Object.CreateItem(expectedScientificWork);
			service.Verify(it => it.CreateItem(expectedScientificWork), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificWork>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificWorks).Returns(mockDbSet.Object);

			var service = new ScientificWorkService(mockContext.Object);

			var scientificWork = GetTestData().First();

			service.CreateItem(scientificWork);
			service.UpdateItem(scientificWork);

			mockDbSet.Verify(m => m.Update(It.IsAny<ScientificWork>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			var scientificWork = GetTestData().First();

			service.Setup(x => x.DeleteById(scientificWork.Id));
			service.Object.DeleteById(scientificWork.Id);
			service.Verify(i => i.DeleteById(scientificWork.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			var scientificWork = GetTestData().First();
			service.Object.CreateItem(scientificWork);

			service.Setup(a => a.Exists(scientificWork.Id));
			service.Object.Exists(scientificWork.Id);
			service.Verify(a => a.Exists(scientificWork.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetAuthorsTest()
		{
			var scientificWork = GetTestData().First();

			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			service.Setup(item => item.GetAuthors(scientificWork.Id));
			service.Object.GetAuthors(scientificWork.Id);
			service.Verify(item => item.GetAuthors(scientificWork.Id));
		}
		
		[Fact]
		public void AddAuthorTest()
		{
			var scientificWork = GetTestData().First();

			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			service.Setup(item => item.AddAuthor(scientificWork.Id, TestData.User3.Id));
			service.Object.AddAuthor(scientificWork.Id, TestData.User3.Id);
			service.Verify(item => item.AddAuthor(scientificWork.Id, TestData.User3.Id));
		}
		
		[Fact]
		public void RemoveAuthorTest()
		{
			var scientificWork = GetTestData().First();

			var service = new Mock<ScientificWorkService>(_mockContext.Object);

			service.Object.AddAuthor(scientificWork.Id, TestData.User3.Id);
			
			service.Setup(item => item.RemoveAuthor(scientificWork.Id, TestData.User3.Id));
			service.Object.RemoveAuthor(scientificWork.Id, TestData.User3.Id);
			service.Verify(item => item.RemoveAuthor(scientificWork.Id, TestData.User3.Id));
		}
	}
}
