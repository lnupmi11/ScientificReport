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
	public class ConferenceRepositoryTests
	{
		private static IEnumerable<Conference> GetTestData()
		{
			return new[]
			{
				TestData.Conference1,
				TestData.Conference2,
				TestData.Conference3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.Conferences).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<ConferenceRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new ConferenceRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.Conference1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<ConferenceRepository>(GetMockContext().Object);

			var conference = GetTestData().First();
			repository.Object.Create(conference);

			repository.Setup(item => item.Get(conference.Id));
			repository.Object.Get(conference.Id);
			repository.Verify(item => item.Get(conference.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<ConferenceRepository>(GetMockContext().Object);

			var conference = GetTestData().First();
			repository.Setup(it => it.Create(conference));
			repository.Object.Create(conference);
			repository.Verify(it => it.Create(conference), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Conference>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Conferences).Returns(mockDbSet.Object);

			var repository = new Mock<ConferenceRepository>(mockContext.Object);

			var conference = GetTestData().First();

			repository.Object.Create(conference);

			repository.Setup(a => a.Update(conference));
			repository.Object.Update(conference);
			repository.Verify(a => a.Update(conference));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Conference>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Conferences).Returns(mockDbSet.Object);

			var repository = new Mock<ConferenceRepository>(mockContext.Object);

			var conference = GetTestData().First();

			repository.Setup(x => x.Delete(conference.Id));
			repository.Object.Delete(conference.Id);
			repository.Verify(i => i.Delete(conference.Id));
		}
	}
}
