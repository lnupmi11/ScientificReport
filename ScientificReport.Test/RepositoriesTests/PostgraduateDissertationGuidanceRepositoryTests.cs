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
	public class PostgraduateDissertationGuidanceRepositoryTests
	{
		private static IEnumerable<PostgraduateDissertationGuidance> GetTestData()
		{
			return new[]
			{
				TestData.PostgraduateDissertationGuidance1,
				TestData.PostgraduateDissertationGuidance2,
				TestData.PostgraduateDissertationGuidance3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<PostgraduateDissertationGuidanceRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new PostgraduateDissertationGuidanceRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.PostgraduateDissertationGuidance1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<PostgraduateDissertationGuidanceRepository>(GetMockContext().Object);

			var postgraduateDissertationGuidance = GetTestData().First();
			repository.Object.Create(postgraduateDissertationGuidance);

			repository.Setup(item => item.Get(postgraduateDissertationGuidance.Id));
			repository.Object.Get(postgraduateDissertationGuidance.Id);
			repository.Verify(item => item.Get(postgraduateDissertationGuidance.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<PostgraduateDissertationGuidanceRepository>(GetMockContext().Object);

			var postgraduateDissertationGuidance = GetTestData().First();
			repository.Setup(it => it.Create(postgraduateDissertationGuidance));
			repository.Object.Create(postgraduateDissertationGuidance);
			repository.Verify(it => it.Create(postgraduateDissertationGuidance), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<PostgraduateDissertationGuidance>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(mockDbSet.Object);

			var repository = new Mock<PostgraduateDissertationGuidanceRepository>(mockContext.Object);

			var postgraduateDissertationGuidance = GetTestData().First();

			repository.Object.Create(postgraduateDissertationGuidance);

			repository.Setup(a => a.Update(postgraduateDissertationGuidance));
			repository.Object.Update(postgraduateDissertationGuidance);
			repository.Verify(a => a.Update(postgraduateDissertationGuidance));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<PostgraduateDissertationGuidance>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PostgraduateDissertationGuidances).Returns(mockDbSet.Object);

			var repository = new Mock<PostgraduateDissertationGuidanceRepository>(mockContext.Object);

			var postgraduateDissertationGuidance = GetTestData().First();

			repository.Setup(x => x.Delete(postgraduateDissertationGuidance.Id));
			repository.Object.Delete(postgraduateDissertationGuidance.Id);
			repository.Verify(i => i.Delete(postgraduateDissertationGuidance.Id));
		}
	}
}
