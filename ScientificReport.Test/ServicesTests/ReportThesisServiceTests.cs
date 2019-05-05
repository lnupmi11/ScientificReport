using System;
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
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

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
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ReportTheses).Returns(MockProvider.GetMockSet(list).Object);
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
			var service = new ReportThesisService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ReportThesis1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<ReportThesisService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<ReportThesisService>(_mockContext.Object);

			var expectedReportThesis = TestData.ReportThesis1;

			service.Setup(it => it.CreateItem(expectedReportThesis));
			service.Object.CreateItem(expectedReportThesis);
			service.Verify(it => it.CreateItem(expectedReportThesis), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<ReportThesis>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ReportTheses).Returns(mockDbSet.Object);

			var service = new ReportThesisService(mockContext.Object);

			var reportThesis = GetTestData().First();

			service.CreateItem(reportThesis);
			service.UpdateItem(reportThesis);

			mockDbSet.Verify(m => m.Update(It.IsAny<ReportThesis>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<ReportThesisService>(_mockContext.Object);

			var reportThesis = GetTestData().First();

			service.Setup(x => x.DeleteById(reportThesis.Id));
			service.Object.DeleteById(reportThesis.Id);
			service.Verify(i => i.DeleteById(reportThesis.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<ReportThesisService>(_mockContext.Object);

			var reportThesis = GetTestData().First();
			service.Object.CreateItem(reportThesis);

			service.Setup(a => a.Exists(reportThesis.Id));
			service.Object.Exists(reportThesis.Id);
			service.Verify(a => a.Exists(reportThesis.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<ReportThesisService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetAuthorsTest()
		{
			var reportThesis = GetTestData().First();

			var service = new Mock<ReportThesisService>(_mockContext.Object);

			service.Setup(item => item.GetAuthors(reportThesis.Id));
			service.Object.GetAuthors(reportThesis.Id);
			service.Verify(item => item.GetAuthors(reportThesis.Id));
		}
	}
}
