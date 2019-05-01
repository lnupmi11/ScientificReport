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
	public class ScientificWorkRepositoryTests
	{
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
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.ScientificWorks).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<ScientificWorkRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new ScientificWorkRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.ScientificWork1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<ScientificWorkRepository>(GetMockContext().Object);

			var scientificWork = GetTestData().First();
			repository.Object.Create(scientificWork);

			repository.Setup(item => item.Get(scientificWork.Id));
			repository.Object.Get(scientificWork.Id);
			repository.Verify(item => item.Get(scientificWork.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<ScientificWorkRepository>(GetMockContext().Object);

			var scientificWork = GetTestData().First();
			repository.Setup(it => it.Create(scientificWork));
			repository.Object.Create(scientificWork);
			repository.Verify(it => it.Create(scientificWork), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificWork>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificWorks).Returns(mockDbSet.Object);

			var repository = new Mock<ScientificWorkRepository>(mockContext.Object);

			var scientificWork = GetTestData().First();

			repository.Object.Create(scientificWork);

			repository.Setup(a => a.Update(scientificWork));
			repository.Object.Update(scientificWork);
			repository.Verify(a => a.Update(scientificWork));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificWork>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificWorks).Returns(mockDbSet.Object);

			var repository = new Mock<ScientificWorkRepository>(mockContext.Object);

			var scientificWork = GetTestData().First();

			repository.Setup(x => x.Delete(scientificWork.Id));
			repository.Object.Delete(scientificWork.Id);
			repository.Verify(i => i.Delete(scientificWork.Id));
		}
	}
}
