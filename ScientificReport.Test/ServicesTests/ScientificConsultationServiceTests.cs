using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.ScientificConsultation;
using Xunit;

namespace ScientificReport.Test.ServicesTests
{
	public class ScientificConsultationServiceTests
	{
		private readonly Mock<DbSet<ScientificConsultation>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<ScientificConsultation> GetTestData()
		{
			return new[]
			{
				TestData.ScientificConsultation1,
				TestData.ScientificConsultation2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificConsultations).Returns(_mockDbSet.Object);
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
			var service = new ScientificConsultationService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.ScientificConsultation1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ScientificConsultationService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new ScientificConsultationService(GetMockContext().Object);

			var expected = TestData.ScientificConsultation3;
			service.CreateItem(new ScientificConsultationModel(expected));

			_mockDbSet.Verify(m => m.Add(It.IsAny<ScientificConsultation>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ScientificConsultationService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.CandidateName = TestData.ScientificConsultation3.CandidateName;
			service.UpdateItem(new ScientificConsultationEditModel(expected));

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ScientificConsultationService(mockContext.Object);

			var item = mockContext.Object.ScientificConsultations.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new ScientificConsultationService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ScientificConsultationService(GetMockContext().Object);

			var item = TestData.ScientificConsultation3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
	}
}
