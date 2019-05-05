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
	public class DepartmentServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<Department> GetTestData()
		{
			return new[]
			{
				TestData.Department1,
				TestData.Department2,
				TestData.Department3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Departments).Returns(MockProvider.GetMockSet(list).Object);
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
			var service = new DepartmentService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Department1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<DepartmentService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var expectedDepartment = TestData.Department1;

			service.Setup(it => it.CreateItem(expectedDepartment));
			service.Object.CreateItem(expectedDepartment);
			service.Verify(it => it.CreateItem(expectedDepartment), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Department>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Departments).Returns(mockDbSet.Object);

			var service = new DepartmentService(mockContext.Object);

			var department = GetTestData().First();

			service.CreateItem(department);
			service.UpdateItem(department);

			mockDbSet.Verify(m => m.Update(It.IsAny<Department>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var department = GetTestData().First();

			service.Setup(x => x.DeleteById(department.Id));
			service.Object.DeleteById(department.Id);

			service.Verify(i => i.DeleteById(department.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var department = GetTestData().First();
			service.Object.CreateItem(department);

			service.Setup(a => a.Exists(department.Id));
			service.Object.Exists(department.Id);
			service.Verify(a => a.Exists(department.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}

		[Fact]
		public void AddScientificWorkTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var department = TestData.Department1;

			service.Setup(it => it.AddScientificWork(department.Id, TestData.ScientificWork2));
			service.Object.AddScientificWork(department.Id, TestData.ScientificWork2);
			service.Verify(it => it.AddScientificWork(department.Id, TestData.ScientificWork2), Times.Once);
		}

		[Fact]
		public void RemoveScientificWorkTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var department = TestData.Department1;

			service.Setup(it => it.RemoveScientificWork(department.Id, TestData.ScientificWork1));
			service.Object.RemoveScientificWork(department.Id, TestData.ScientificWork1);
			service.Verify(it => it.RemoveScientificWork(department.Id, TestData.ScientificWork1), Times.Once);
		}
		
		[Fact]
		public void AddUserTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var department = TestData.Department1;

			service.Setup(it => it.AddUser(department.Id, TestData.User3));
			service.Object.AddUser(department.Id, TestData.User3);
			service.Verify(it => it.AddUser(department.Id, TestData.User3), Times.Once);
		}

		[Fact]
		public void RemoveUserTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			var department = TestData.Department1;

			service.Setup(it => it.RemoveUser(department.Id, TestData.User1));
			service.Object.RemoveUser(department.Id, TestData.User1);
			service.Verify(it => it.RemoveUser(department.Id, TestData.User1), Times.Once);
		}

		[Fact]
		public void UserIsHiredTest()
		{
			var service = new Mock<DepartmentService>(_mockContext.Object);

			service.Setup(it => it.UserIsHired(TestData.User1));
			service.Object.UserIsHired(TestData.User1);
			service.Verify(it => it.UserIsHired(TestData.User1), Times.Once);
			
			service.Setup(it => it.UserIsHired(TestData.User2));
			service.Object.UserIsHired(TestData.User2);
			service.Verify(it => it.UserIsHired(TestData.User2), Times.Once);
		}
	}
}
