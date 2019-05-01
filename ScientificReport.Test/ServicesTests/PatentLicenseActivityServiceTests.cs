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
	public class PatentLicenseActivityServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();

		private static IEnumerable<PatentLicenseActivity> GetTestData()
		{
			return new[]
			{
				TestData.PatentLicenseActivity1,
				TestData.PatentLicenseActivity2,
				TestData.PatentLicenseActivity3
			};
		}

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PatentLicenseActivities).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.PatentLicenseActivities).Returns(MockProvider.GetMockSet(list).Object);

			var service = new PatentLicenseActivityService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new PatentLicenseActivityService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.PatentLicenseActivity1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			var expectedPatentLicenseActivity = TestData.PatentLicenseActivity1;

			service.Setup(it => it.CreateItem(expectedPatentLicenseActivity));
			service.Object.CreateItem(expectedPatentLicenseActivity);
			service.Verify(it => it.CreateItem(expectedPatentLicenseActivity), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<PatentLicenseActivity>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.PatentLicenseActivities).Returns(mockDbSet.Object);

			var service = new PatentLicenseActivityService(mockContext.Object);

			var patentLicenseActivity = GetTestData().First();

			service.CreateItem(patentLicenseActivity);
			service.UpdateItem(patentLicenseActivity);

			mockDbSet.Verify(m => m.Update(It.IsAny<PatentLicenseActivity>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			var patentLicenseActivity = GetTestData().First();

			service.Setup(x => x.DeleteById(patentLicenseActivity.Id));
			service.Object.DeleteById(patentLicenseActivity.Id);
			service.Verify(i => i.DeleteById(patentLicenseActivity.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			var patentLicenseActivity = GetTestData().First();
			service.Object.CreateItem(patentLicenseActivity);

			service.Setup(a => a.Exists(patentLicenseActivity.Id));
			service.Object.Exists(patentLicenseActivity.Id);
			service.Verify(a => a.Exists(patentLicenseActivity.Id));
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetAuthorsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			service.Setup(item => item.GetAuthors(patentLicenseActivity.Id));
			service.Object.GetAuthors(patentLicenseActivity.Id);
			service.Verify(item => item.GetAuthors(patentLicenseActivity.Id));
		}

		[Fact]
		public void GetApplicantsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			service.Setup(item => item.GetApplicants(patentLicenseActivity.Id));
			service.Object.GetApplicants(patentLicenseActivity.Id);
			service.Verify(item => item.GetApplicants(patentLicenseActivity.Id));
		}

		[Fact]
		public void GetCoauthorsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			service.Setup(item => item.GetCoauthors(patentLicenseActivity.Id));
			service.Object.GetCoauthors(patentLicenseActivity.Id);
			service.Verify(item => item.GetCoauthors(patentLicenseActivity.Id));
		}
		
		[Fact]
		public void GetCoApplicantsTest()
		{
			var patentLicenseActivity = GetTestData().First();

			var service = new Mock<PatentLicenseActivityService>(_mockContext.Object);

			service.Setup(item => item.GetCoApplicants(patentLicenseActivity.Id));
			service.Object.GetCoApplicants(patentLicenseActivity.Id);
			service.Verify(item => item.GetCoApplicants(patentLicenseActivity.Id));
		}
	}
}
