using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class DepartmentServiceTests
	{
		private readonly Mock<DbSet<Department>> _mockDbSetDepartments = MockProvider.GetMockSet(GetTestDepartments().AsQueryable());
		private readonly Mock<DbSet<ScientificWork>> _mockDbSetScientificWorks = MockProvider.GetMockSet(GetTestScientificWorks().AsQueryable());
		private readonly Mock<DbSet<UserProfile>> _mockDbSetUserProfiles = MockProvider.GetMockSet(GetTestUserProfiles().AsQueryable());

		private static IEnumerable<Department> GetTestDepartments()
		{
			return new[]
			{
				TestData.Department1,
				TestData.Department2
			};
		}
		
		private static IEnumerable<ScientificWork> GetTestScientificWorks()
		{
			return new[]
			{
				TestData.ScientificWork1,
				TestData.ScientificWork2
			};
		}
		
		private static IEnumerable<UserProfile> GetTestUserProfiles()
		{
			return new[]
			{
				TestData.User1,
				TestData.User2,
				TestData.User3
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Departments).Returns(_mockDbSetDepartments.Object);
			mockContext.Setup(item => item.ScientificWorks).Returns(_mockDbSetScientificWorks.Object);
			mockContext.Setup(item => item.UserProfiles).Returns(_mockDbSetUserProfiles.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestDepartments().AsQueryable();

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
			var expected = GetTestDepartments().First();
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

			_mockDbSetDepartments.Verify(m => m.Add(It.IsAny<Department>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new DepartmentService(GetMockContext().Object);

			var expected = GetTestDepartments().First();
			expected.Title = TestData.Department3.Title;
			service.UpdateItem(expected);

			_mockDbSetDepartments.Verify(m => m.Update(expected), Times.Once);
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

			var item = GetTestDepartments().First();
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
			var mockContext = GetMockContext();
			var service = new DepartmentService(GetMockContext().Object);

			var department = mockContext.Object.Departments.First();
			Assert.Equal(0, department.ScientificWorks.Count);

			var sw = mockContext.Object.ScientificWorks.Last();
			service.AddScientificWork(department.Id, sw);
			
			department = _mockDbSetDepartments.Object.First();
			Assert.Equal(1, department.ScientificWorks.Count);
		}

		[Fact]
		public void RemoveScientificWorkTest()
		{
			var mockContext = GetMockContext();
			var service = new DepartmentService(mockContext.Object);

			var department = mockContext.Object.Departments.First();
			Assert.Equal(1, department.ScientificWorks.Count);
			
			var sw = mockContext.Object.ScientificWorks.First();
			service.RemoveScientificWork(department.Id, sw);
			
			department = mockContext.Object.Departments.First();
			Assert.Equal(0, department.ScientificWorks.Count);
		}

		[Fact]
		public void AddUserTest()
		{
			var mockContext = GetMockContext();
			var service = new DepartmentService(mockContext.Object);

			var department = mockContext.Object.Departments.First();

			service.AddUser(department.Id, TestData.User3);
			
			department = mockContext.Object.Departments.First();
			
			Assert.Equal(3, department.Staff.Count);
		}

		[Fact]
		public void RemoveUserTest()
		{
			var mockContext = GetMockContext();
			var service = new DepartmentService(mockContext.Object);

			var department = mockContext.Object.Departments.First();
			Assert.Equal(3, department.Staff.Count);

			var user1 = mockContext.Object.UserProfiles.First();
			
			service.RemoveUser(department.Id, user1);
			department = mockContext.Object.Departments.First();
			
			Assert.Equal(2, department.Staff.Count);
		}

		[Fact]
		public void UserIsHiredTest()
		{
			var mockContext = GetMockContext();
			var service = new DepartmentService(mockContext.Object);

			var actual = service.UserIsHired(TestData.User3);
			Assert.True(actual);
			
			actual = service.UserIsHired(TestData.User2);
			Assert.False(actual);
		}
		
		[Fact]
		public void GetPageTest()
		{
			var service = new DepartmentService(GetMockContext().Object);
			var expected = _mockDbSetDepartments.Object.AsQueryable();
			
			Assert.NotNull(expected);
			
			var count = expected.Count();
			var actual = service.GetPage(1, count);
			
			Assert.NotNull(actual);
			Assert.Equal(count, actual.Count());
		}
		
		[Fact]
		public void GetCountTest()
		{
			var service = new DepartmentService(GetMockContext().Object);
			var expected = _mockDbSetDepartments.Object.AsQueryable();
			
			Assert.Equal(expected.Count(), service.GetCount());
		}
	}
}
