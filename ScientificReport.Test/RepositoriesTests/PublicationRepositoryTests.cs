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
	public class PublicationRepositoryTests
	{
		private static IEnumerable<Publication> GetTestData()
		{
			return new[]
			{
				TestData.Publication1,
				TestData.Publication2,
				TestData.Publication3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.Publications).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<PublicationRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new PublicationRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.Publication1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<PublicationRepository>(GetMockContext().Object);

			var publication = GetTestData().First();
			repository.Object.Create(publication);

			repository.Setup(item => item.Get(publication.Id));
			repository.Object.Get(publication.Id);
			repository.Verify(item => item.Get(publication.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<PublicationRepository>(GetMockContext().Object);

			var publication = GetTestData().First();
			repository.Setup(it => it.Create(publication));
			repository.Object.Create(publication);
			repository.Verify(it => it.Create(publication), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var repository = new Mock<PublicationRepository>(mockContext.Object);

			var publication = GetTestData().First();

			repository.Object.Create(publication);

			repository.Setup(a => a.Update(publication));
			repository.Object.Update(publication);
			repository.Verify(a => a.Update(publication));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var repository = new Mock<PublicationRepository>(mockContext.Object);

			var publication = GetTestData().First();

			repository.Setup(x => x.Delete(publication.Id));
			repository.Object.Delete(publication.Id);
			repository.Verify(i => i.Delete(publication.Id));
		}
	}
}
