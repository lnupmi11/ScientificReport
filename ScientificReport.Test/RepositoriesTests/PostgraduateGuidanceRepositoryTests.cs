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
	public class PostgraduateGuidanceRepositoryTests
	{
		private static IEnumerable<PostgraduateGuidance> GetTestData()
		{
			return new[]
			{
				TestData.PostgraduateGuidance1,
				TestData.PostgraduateGuidance2,
				TestData.PostgraduateGuidance3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.PostgraduateGuidances).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<PostgraduateGuidanceRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new PostgraduateGuidanceRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.PostgraduateGuidance1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<PostgraduateGuidanceRepository>(GetMockContext().Object);

			var postgraduateGuidance = GetTestData().First();
			repository.Object.Create(postgraduateGuidance);

			repository.Setup(item => item.Get(postgraduateGuidance.Id));
			repository.Object.Get(postgraduateGuidance.Id);
			repository.Verify(item => item.Get(postgraduateGuidance.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<PostgraduateGuidanceRepository>(GetMockContext().Object);

			var postgraduateGuidance = GetTestData().First();
			repository.Setup(it => it.Create(postgraduateGuidance));
			repository.Object.Create(postgraduateGuidance);
			repository.Verify(it => it.Create(postgraduateGuidance), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<PostgraduateGuidance>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PostgraduateGuidances).Returns(mockDbSet.Object);

			var repository = new Mock<PostgraduateGuidanceRepository>(mockContext.Object);

			var postgraduateGuidance = GetTestData().First();

			repository.Object.Create(postgraduateGuidance);

			repository.Setup(a => a.Update(postgraduateGuidance));
			repository.Object.Update(postgraduateGuidance);
			repository.Verify(a => a.Update(postgraduateGuidance));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<PostgraduateGuidance>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PostgraduateGuidances).Returns(mockDbSet.Object);

			var repository = new Mock<PostgraduateGuidanceRepository>(mockContext.Object);

			var postgraduateGuidance = GetTestData().First();

			repository.Setup(x => x.Delete(postgraduateGuidance.Id));
			repository.Object.Delete(postgraduateGuidance.Id);
			repository.Verify(i => i.Delete(postgraduateGuidance.Id));
		}
	}
}
