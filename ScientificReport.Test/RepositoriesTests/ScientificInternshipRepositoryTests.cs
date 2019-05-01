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
	public class ScientificInternshipRepositoryTests
	{
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
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.ScientificInternships).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<ScientificInternshipRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new ScientificInternshipRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.ScientificInternship1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<ScientificInternshipRepository>(GetMockContext().Object);

			var scientificInternship = GetTestData().First();
			repository.Object.Create(scientificInternship);

			repository.Setup(item => item.Get(scientificInternship.Id));
			repository.Object.Get(scientificInternship.Id);
			repository.Verify(item => item.Get(scientificInternship.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<ScientificInternshipRepository>(GetMockContext().Object);

			var scientificInternship = GetTestData().First();
			repository.Setup(it => it.Create(scientificInternship));
			repository.Object.Create(scientificInternship);
			repository.Verify(it => it.Create(scientificInternship), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificInternship>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificInternships).Returns(mockDbSet.Object);

			var repository = new Mock<ScientificInternshipRepository>(mockContext.Object);

			var scientificInternship = GetTestData().First();

			repository.Object.Create(scientificInternship);

			repository.Setup(a => a.Update(scientificInternship));
			repository.Object.Update(scientificInternship);
			repository.Verify(a => a.Update(scientificInternship));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificInternship>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificInternships).Returns(mockDbSet.Object);

			var repository = new Mock<ScientificInternshipRepository>(mockContext.Object);

			var scientificInternship = GetTestData().First();

			repository.Setup(x => x.Delete(scientificInternship.Id));
			repository.Object.Delete(scientificInternship.Id);
			repository.Verify(i => i.Delete(scientificInternship.Id));
		}
	}
}
