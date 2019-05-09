using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class ReportThesisServiceTests
	{
		private readonly Mock<DbSet<ReportThesis>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<ReportThesis> GetTestData()
		{
			return new[]
			{
				TestData.ReportThesis1,
				TestData.ReportThesis2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ReportTheses).Returns(_mockDbSet.Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ReportTheses).Returns(MockProvider.GetMockSet(list).Object);
			var service = new ReportThesisService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ReportThesisService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ReportThesis1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ReportThesisService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new ReportThesisService(GetMockContext().Object);

			var expected = TestData.ReportThesis3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<ReportThesis>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ReportThesisService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Thesis = TestData.ReportThesis3.Thesis;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ReportThesisService(mockContext.Object);

			var item = mockContext.Object.ReportTheses.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new ReportThesisService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ReportThesisService(GetMockContext().Object);

			var item = TestData.ReportThesis3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
		
		[Fact]
		public void GetAuthorsTest()
		{
			var reportThesis = GetTestData().First();

			var service = new Mock<ReportThesisService>(GetMockContext().Object);

			service.Setup(item => item.GetAuthors(reportThesis.Id));
			service.Object.GetAuthors(reportThesis.Id);
			service.Verify(item => item.GetAuthors(reportThesis.Id));
		}
	}
}
