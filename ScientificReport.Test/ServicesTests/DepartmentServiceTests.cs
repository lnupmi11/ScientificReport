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
	public class DepartmentServiceTests
	{
		private readonly Mock<DbSet<Department>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<Department> GetTestData()
		{
			return new[]
			{
				TestData.Department1,
				TestData.Department2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Departments).Returns(_mockDbSet.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Departments).Returns(MockProvider.GetMockSet(list).Object);
			var service = new DepartmentService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new DepartmentService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Department1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new DepartmentService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new DepartmentService(GetMockContext().Object);

			var expected = TestData.Department3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<Department>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new DepartmentService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Title = TestData.Department3.Title;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new DepartmentService(mockContext.Object);

			var item = mockContext.Object.Departments.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new DepartmentService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new DepartmentService(GetMockContext().Object);

			var item = TestData.Department3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}

		[Fact]
		public void AddScientificWorkTest()
		{
			var service = new Mock<DepartmentService>(GetMockContext().Object);

			var department = TestData.Department1;

			service.Setup(it => it.AddScientificWork(department.Id, TestData.ScientificWork2));
			service.Object.AddScientificWork(department.Id, TestData.ScientificWork2);
			service.Verify(it => it.AddScientificWork(department.Id, TestData.ScientificWork2), Times.Once);
		}

		[Fact]
		public void RemoveScientificWorkTest()
		{
			var service = new Mock<DepartmentService>(GetMockContext().Object);

			var department = TestData.Department1;

			service.Setup(it => it.RemoveScientificWork(department.Id, TestData.ScientificWork1));
			service.Object.RemoveScientificWork(department.Id, TestData.ScientificWork1);
			service.Verify(it => it.RemoveScientificWork(department.Id, TestData.ScientificWork1), Times.Once);
		}
		
		[Fact]
		public void AddUserTest()
		{
			var service = new Mock<DepartmentService>(GetMockContext().Object);

			var department = TestData.Department1;

			service.Setup(it => it.AddUser(department.Id, TestData.User3));
			service.Object.AddUser(department.Id, TestData.User3);
			service.Verify(it => it.AddUser(department.Id, TestData.User3), Times.Once);
		}

		[Fact]
		public void RemoveUserTest()
		{
			var service = new Mock<DepartmentService>(GetMockContext().Object);

			var department = TestData.Department1;

			service.Setup(it => it.RemoveUser(department.Id, TestData.User1));
			service.Object.RemoveUser(department.Id, TestData.User1);
			service.Verify(it => it.RemoveUser(department.Id, TestData.User1), Times.Once);
		}

		[Fact]
		public void UserIsHiredTest()
		{
			var service = new Mock<DepartmentService>(GetMockContext().Object);

			service.Setup(it => it.UserIsHired(TestData.User1));
			service.Object.UserIsHired(TestData.User1);
			service.Verify(it => it.UserIsHired(TestData.User1), Times.Once);
			
			service.Setup(it => it.UserIsHired(TestData.User2));
			service.Object.UserIsHired(TestData.User2);
			service.Verify(it => it.UserIsHired(TestData.User2), Times.Once);
		}
	}
}
