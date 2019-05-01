using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class TeacherReportRepositoryTests
	{
		private static IEnumerable<TeacherReport> GetTestData()
		{
			return new[]
			{
				TestData.TeacherReport1,
				TestData.TeacherReport2,
				TestData.TeacherReport3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
        {
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.TeacherReports).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<TeacherReportRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new TeacherReportRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.TeacherReport1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<TeacherReportRepository>(GetMockContext().Object);

			var teacherReport = GetTestData().First();
			repository.Object.Create(teacherReport);

			repository.Setup(item => item.Get(teacherReport.Id));
			repository.Object.Get(teacherReport.Id);
			repository.Verify(item => item.Get(teacherReport.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<TeacherReportRepository>(GetMockContext().Object);

			var teacherReport = GetTestData().First();
			repository.Setup(it => it.Create(teacherReport));
			repository.Object.Create(teacherReport);
			repository.Verify(it => it.Create(teacherReport), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<TeacherReport>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.TeacherReports).Returns(mockDbSet.Object);

			var repository = new Mock<TeacherReportRepository>(mockContext.Object);

			var teacherReport = GetTestData().First();

			repository.Object.Create(teacherReport);

			repository.Setup(a => a.Update(teacherReport));
			repository.Object.Update(teacherReport);
			repository.Verify(a => a.Update(teacherReport));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<TeacherReport>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.TeacherReports).Returns(mockDbSet.Object);

			var repository = new Mock<TeacherReportRepository>(mockContext.Object);

			var teacherReport = GetTestData().First();

			repository.Setup(x => x.Delete(teacherReport.Id));
			repository.Object.Delete(teacherReport.Id);
			repository.Verify(i => i.Delete(teacherReport.Id));
		}
	}
}
