using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class TeacherReportServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

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
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.TeacherReports).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.TeacherReports).Returns(MockProvider.GetMockSet(list).Object);

			var service = new TeacherReportService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new TeacherReportService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.TeacherReport1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<TeacherReportService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<TeacherReportService>(_mockContext.Object);

			var expectedTeacherReport = TestData.TeacherReport1;

			service.Setup(it => it.CreateItem(expectedTeacherReport));
			service.Object.CreateItem(expectedTeacherReport);
			service.Verify(it => it.CreateItem(expectedTeacherReport), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<TeacherReport>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.TeacherReports).Returns(mockDbSet.Object);

			var service = new TeacherReportService(mockContext.Object);

			var teacherReport = GetTestData().First();

			service.CreateItem(teacherReport);
			service.UpdateItem(teacherReport);

			mockDbSet.Verify(m => m.Update(It.IsAny<TeacherReport>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<TeacherReportService>(_mockContext.Object);

			var teacherReport = GetTestData().First();

			service.Setup(x => x.DeleteById(teacherReport.Id));
			service.Object.DeleteById(teacherReport.Id);
			service.Verify(i => i.DeleteById(teacherReport.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<TeacherReportService>(_mockContext.Object);

			var teacherReport = GetTestData().First();
			service.Object.CreateItem(teacherReport);

			service.Setup(a => a.Exists(teacherReport.Id));
			service.Object.Exists(teacherReport.Id);
			service.Verify(a => a.Exists(teacherReport.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<TeacherReportService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
	}
}
