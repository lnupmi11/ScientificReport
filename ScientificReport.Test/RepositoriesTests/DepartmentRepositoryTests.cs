using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class DepartmentRepositoryTests
	{
		private static readonly IEnumerable<Department> TestDepartments = new[]
		{
			TestData.Department1,
			TestData.Department2,
			TestData.Department3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Departments).Returns(
				MockProvider.GetMockSet(TestDepartments).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new DepartmentRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestDepartments.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Departments.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			var expected = mockContext.Object.Departments.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.Departments.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			var expected = mockContext.Object.Departments.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			Assert.Equal(TestDepartments.Count(), mockContext.Object.Departments.Count());
			repository.Create(TestData.Department1);
			Assert.Equal(TestDepartments.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			var item = mockContext.Object.Departments.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new DepartmentRepository(mockContext.Object);
			var item = mockContext.Object.Departments.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Departments.Find(item.Id));
		}
	}
}
