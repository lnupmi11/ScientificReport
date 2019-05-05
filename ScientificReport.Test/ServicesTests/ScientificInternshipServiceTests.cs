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
	public class ScientificInternshipServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<ScientificInternship> GetTestData()
		{
			return new[]
			{
				TestData.ScientificInternship1,
				TestData.ScientificInternship2,
				TestData.ScientificInternship3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificInternships).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificInternships).Returns(MockProvider.GetMockSet(list).Object);

			var service = new ScientificInternshipService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ScientificInternshipService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ScientificInternship1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<ScientificInternshipService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<ScientificInternshipService>(_mockContext.Object);

			var expectedScientificInternship = TestData.ScientificInternship1;

			service.Setup(it => it.CreateItem(expectedScientificInternship));
			service.Object.CreateItem(expectedScientificInternship);
			service.Verify(it => it.CreateItem(expectedScientificInternship), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificInternship>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificInternships).Returns(mockDbSet.Object);

			var service = new ScientificInternshipService(mockContext.Object);

			var scientificInternship = GetTestData().First();

			service.CreateItem(scientificInternship);
			service.UpdateItem(scientificInternship);

			mockDbSet.Verify(m => m.Update(It.IsAny<ScientificInternship>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<ScientificInternshipService>(_mockContext.Object);

			var scientificInternship = GetTestData().First();

			service.Setup(x => x.DeleteById(scientificInternship.Id));
			service.Object.DeleteById(scientificInternship.Id);
			service.Verify(i => i.DeleteById(scientificInternship.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<ScientificInternshipService>(_mockContext.Object);

			var scientificInternship = GetTestData().First();
			service.Object.CreateItem(scientificInternship);

			service.Setup(a => a.Exists(scientificInternship.Id));
			service.Object.Exists(scientificInternship.Id);
			service.Verify(a => a.Exists(scientificInternship.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<ScientificInternshipService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetUsersTest()
		{
			var scientificInternship = GetTestData().First();

			var service = new Mock<ScientificInternshipService>(_mockContext.Object);

			service.Setup(item => item.GetUsers(scientificInternship.Id));
			service.Object.GetUsers(scientificInternship.Id);
			service.Verify(item => item.GetUsers(scientificInternship.Id));
		}
	}
}
