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
	public class OppositionRepositoryTests
	{
		private static IEnumerable<Opposition> GetTestData()
		{
			return new[]
			{
				TestData.Opposition1,
				TestData.Opposition2,
				TestData.Opposition3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.Oppositions).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<OppositionRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new OppositionRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.Opposition1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<OppositionRepository>(GetMockContext().Object);

			var opposition = GetTestData().First();
			repository.Object.Create(opposition);

			repository.Setup(item => item.Get(opposition.Id));
			repository.Object.Get(opposition.Id);
			repository.Verify(item => item.Get(opposition.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<OppositionRepository>(GetMockContext().Object);

			var opposition = GetTestData().First();
			repository.Setup(it => it.Create(opposition));
			repository.Object.Create(opposition);
			repository.Verify(it => it.Create(opposition), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Opposition>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Oppositions).Returns(mockDbSet.Object);

			var repository = new Mock<OppositionRepository>(mockContext.Object);

			var opposition = GetTestData().First();

			repository.Object.Create(opposition);

			repository.Setup(a => a.Update(opposition));
			repository.Object.Update(opposition);
			repository.Verify(a => a.Update(opposition));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Opposition>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Oppositions).Returns(mockDbSet.Object);

			var repository = new Mock<OppositionRepository>(mockContext.Object);

			var opposition = GetTestData().First();

			repository.Setup(x => x.Delete(opposition.Id));
			repository.Object.Delete(opposition.Id);
			repository.Verify(i => i.Delete(opposition.Id));
		}
	}
}
