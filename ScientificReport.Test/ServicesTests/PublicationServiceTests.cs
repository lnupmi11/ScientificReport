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
	public class PublicationServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();
		
		private static IEnumerable<Publication> GetTestData()
		{
			return new[]
			{
				TestData.Publication1,
				TestData.Publication2,
				TestData.Publication3
			};
		}
		
		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();
			
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(MockProvider.GetMockSet(list).Object);

			var service = new PublicationService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new PublicationService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Publication1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			
			var service = new Mock<PublicationService>(_mockContext.Object);
			
			service.Object.CreateItem(TestData.User1, expected);
			
			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<PublicationService>(_mockContext.Object);

			var expectedPublication = TestData.Publication1;
			var user = TestData.User1;
			
			service.Setup(it => it.CreateItem(user, expectedPublication));
			service.Object.CreateItem(user, expectedPublication);
			service.Verify(it => it.CreateItem(user, expectedPublication), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var service = new PublicationService(mockContext.Object);

			var publication = GetTestData().First();

			var user = TestData.User1;
			
			service.CreateItem(user, publication);
			service.UpdateItem(user, publication);
			
			mockDbSet.Verify(m => m.Update(It.IsAny<Publication>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<PublicationService>(_mockContext.Object);

			var publication = GetTestData().First();
			
			service.Setup(x => x.DeleteById(publication.Id));
			service.Object.DeleteById(publication.Id);
			service.Verify(i => i.DeleteById(publication.Id));
		}

		[Fact]
		public void PublicationExistsTest()
		{
			var service = new Mock<PublicationService>(_mockContext.Object);

			var publication = GetTestData().First();
			service.Object.CreateItem(TestData.User1, publication);
			
			service.Setup(a => a.PublicationExists(publication.Id));
			service.Object.PublicationExists(publication.Id);
			service.Verify(a => a.PublicationExists(publication.Id));
		}
		
		[Fact]
		public void PublicationDoesNotExistTest()
		{
			var service = new Mock<PublicationService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.PublicationExists(guid));
			service.Object.PublicationExists(guid);
			service.Verify(a => a.PublicationExists(guid));
		}
		
		[Fact]
		public void GetPublicationAuthorsTest()
		{
			var service = new Mock<PublicationService>(_mockContext.Object);

			service.Setup(a => a.PublicationExists(TestData.Publication1.Id));
			service.Object.PublicationExists(TestData.Publication1.Id);
			service.Verify(a => a.PublicationExists(TestData.Publication1.Id));
		}
	}
}
