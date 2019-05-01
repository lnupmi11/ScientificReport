using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class DepartmentRepositoryTests
	{
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
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.Departments).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<DepartmentRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new DepartmentRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.Department1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<DepartmentRepository>(GetMockContext().Object);

			var department = GetTestData().First();
			repository.Object.Create(department);

			repository.Setup(item => item.Get(department.Id));
			repository.Object.Get(department.Id);
			repository.Verify(item => item.Get(department.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<DepartmentRepository>(GetMockContext().Object);

			var department = GetTestData().First();
			repository.Setup(it => it.Create(department));
			repository.Object.Create(department);
			repository.Verify(it => it.Create(department), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Department>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Departments).Returns(mockDbSet.Object);

			var repository = new Mock<DepartmentRepository>(mockContext.Object);

			var department = GetTestData().First();

			repository.Object.Create(department);

			repository.Setup(a => a.Update(department));
			repository.Object.Update(department);
			repository.Verify(a => a.Update(department));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Department>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Departments).Returns(mockDbSet.Object);

			var repository = new Mock<DepartmentRepository>(mockContext.Object);

			var department = GetTestData().First();

			repository.Setup(x => x.Delete(department.Id));
			repository.Object.Delete(department.Id);
			repository.Verify(i => i.Delete(department.Id));
		}
	}
}
