using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.ScientificInternship;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class ScientificInternshipServiceTests
	{
		private readonly Mock<DbSet<ScientificInternship>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<ScientificInternship> GetTestData()
		{
			return new[]
			{
				TestData.ScientificInternship1,
				TestData.ScientificInternship2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificInternships).Returns(_mockDbSet.Object);
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
			var service = new ScientificInternshipService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ScientificInternship1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ScientificInternshipService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new ScientificInternshipService(GetMockContext().Object);

			var expected = TestData.ScientificInternship3;
			service.CreateItem(new ScientificInternshipModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<ScientificInternship>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ScientificInternshipService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Contents = TestData.ScientificInternship3.Contents;
			service.UpdateItem(new ScientificInternshipEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ScientificInternshipService(mockContext.Object);

			var item = mockContext.Object.ScientificInternships.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new ScientificInternshipService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ScientificInternshipService(GetMockContext().Object);

			var item = TestData.ScientificInternship3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
		
		[Fact]
		public void GetUsersTest()
		{
			var scientificInternship = GetTestData().First();

			var service = new Mock<ScientificInternshipService>(GetMockContext().Object);

			service.Setup(item => item.GetUsers(scientificInternship.Id));
			service.Object.GetUsers(scientificInternship.Id);
			service.Verify(item => item.GetUsers(scientificInternship.Id));
		}
	}
}
