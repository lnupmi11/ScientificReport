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
	public class ScientificConsultationServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<ScientificConsultation> GetTestData()
		{
			return new[]
			{
				TestData.ScientificConsultation1,
				TestData.ScientificConsultation2,
				TestData.ScientificConsultation3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificConsultations).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificConsultations).Returns(MockProvider.GetMockSet(list).Object);

			var service = new ScientificConsultationService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ScientificConsultationService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ScientificConsultation1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<ScientificConsultationService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<ScientificConsultationService>(_mockContext.Object);

			var expectedScientificConsultation = TestData.ScientificConsultation1;

			service.Setup(it => it.CreateItem(expectedScientificConsultation));
			service.Object.CreateItem(expectedScientificConsultation);
			service.Verify(it => it.CreateItem(expectedScientificConsultation), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificConsultation>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificConsultations).Returns(mockDbSet.Object);

			var service = new ScientificConsultationService(mockContext.Object);

			var scientificConsultation = GetTestData().First();

			service.CreateItem(scientificConsultation);
			service.UpdateItem(scientificConsultation);

			mockDbSet.Verify(m => m.Update(It.IsAny<ScientificConsultation>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<ScientificConsultationService>(_mockContext.Object);

			var scientificConsultation = GetTestData().First();

			service.Setup(x => x.DeleteById(scientificConsultation.Id));
			service.Object.DeleteById(scientificConsultation.Id);
			service.Verify(i => i.DeleteById(scientificConsultation.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<ScientificConsultationService>(_mockContext.Object);

			var scientificConsultation = GetTestData().First();
			service.Object.CreateItem(scientificConsultation);

			service.Setup(a => a.Exists(scientificConsultation.Id));
			service.Object.Exists(scientificConsultation.Id);
			service.Verify(a => a.Exists(scientificConsultation.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<ScientificConsultationService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
	}
}
