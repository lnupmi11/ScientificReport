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
		private readonly Mock<DbSet<TeacherReport>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<TeacherReport> GetTestData()
		{
			return new[]
			{
				TestData.TeacherReport1,
				TestData.TeacherReport2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.TeacherReports).Returns(_mockDbSet.Object);
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
			var service = new TeacherReportService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.TeacherReport1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new TeacherReportService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new TeacherReportService(GetMockContext().Object);

			var expected = TestData.TeacherReport3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<TeacherReport>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new TeacherReportService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Teacher = TestData.TeacherReport3.Teacher;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new TeacherReportService(mockContext.Object);

			var item = mockContext.Object.TeacherReports.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new TeacherReportService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new TeacherReportService(GetMockContext().Object);

			var item = TestData.TeacherReport3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
	}
}
