using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Opposition;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class OppositionServiceTests
	{
		private readonly Mock<DbSet<Opposition>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<Opposition> GetTestData()
		{
			return new[]
			{
				TestData.Opposition1,
				TestData.Opposition2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Oppositions).Returns(_mockDbSet.Object);
			
			var userProfileSet = MockProvider.GetMockSet(new []{TestData.User1}.AsQueryable());
			var departmentSet = MockProvider.GetMockSet(new []{TestData.Department1}.AsQueryable());
			
			mockContext.Setup(item => item.UserProfiles).Returns(userProfileSet.Object);
			mockContext.Setup(item => item.Departments).Returns(departmentSet.Object);
			
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Oppositions).Returns(MockProvider.GetMockSet(list).Object);
			var service = new OppositionService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new OppositionService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Opposition1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new OppositionService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new OppositionService(GetMockContext().Object);

			var expected = TestData.Opposition3;
			service.CreateItem(new OppositionModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<Opposition>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new OppositionService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.About = TestData.Opposition3.About;
			service.UpdateItem(new OppositionEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new OppositionService(mockContext.Object);

			var item = mockContext.Object.Oppositions.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new OppositionService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new OppositionService(GetMockContext().Object);

			var item = TestData.Opposition3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
	}
}
