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
	public class ReportThesisRepositoryTests
	{
		private static IEnumerable<ReportThesis> GetTestData()
		{
			return new[]
			{
				TestData.ReportThesis1,
				TestData.ReportThesis2,
				TestData.ReportThesis3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.ReportTheses).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<ReportThesisRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new ReportThesisRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.ReportThesis1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<ReportThesisRepository>(GetMockContext().Object);

			var reportThesis = GetTestData().First();
			repository.Object.Create(reportThesis);

			repository.Setup(item => item.Get(reportThesis.Id));
			repository.Object.Get(reportThesis.Id);
			repository.Verify(item => item.Get(reportThesis.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<ReportThesisRepository>(GetMockContext().Object);

			var reportThesis = GetTestData().First();
			repository.Setup(it => it.Create(reportThesis));
			repository.Object.Create(reportThesis);
			repository.Verify(it => it.Create(reportThesis), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<ReportThesis>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ReportTheses).Returns(mockDbSet.Object);

			var repository = new Mock<ReportThesisRepository>(mockContext.Object);

			var reportThesis = GetTestData().First();

			repository.Object.Create(reportThesis);

			repository.Setup(a => a.Update(reportThesis));
			repository.Object.Update(reportThesis);
			repository.Verify(a => a.Update(reportThesis));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<ReportThesis>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ReportTheses).Returns(mockDbSet.Object);

			var repository = new Mock<ReportThesisRepository>(mockContext.Object);

			var reportThesis = GetTestData().First();

			repository.Setup(x => x.Delete(reportThesis.Id));
			repository.Object.Delete(reportThesis.Id);
			repository.Verify(i => i.Delete(reportThesis.Id));
		}
	}
}
