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
	public class ConferenceServiceTests
	{
		private readonly Mock<ScientificReportDbContext> _mockContext = GetMockContext();
		
		private static IEnumerable<Conference> GetTestData()
		{
			return new[]
			{
				TestData.Conference1,
				TestData.Conference2,
				TestData.Conference3
			};
		}
		
		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var list = GetTestData().AsQueryable();
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Conferences).Returns(MockProvider.GetMockSet(list).Object);
			return mockContext;
		}

		[Fact]
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Conferences).Returns(MockProvider.GetMockSet(list).Object);

			var service = new ConferenceService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}

		[Fact]
		public void GetAllWhereTest()
		{
			var service = new ConferenceService(_mockContext.Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Conference1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();

			var service = new Mock<ConferenceService>(_mockContext.Object);

			service.Object.CreateItem(expected);

			service.Setup(item => item.GetById(expected.Id));
			service.Object.GetById(expected.Id);
			service.Verify(item => item.GetById(expected.Id));
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new Mock<ConferenceService>(_mockContext.Object);

			var expectedConference = TestData.Conference1;

			service.Setup(it => it.CreateItem(expectedConference));
			service.Object.CreateItem(expectedConference);
			service.Verify(it => it.CreateItem(expectedConference), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Conference>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Conferences).Returns(mockDbSet.Object);

			var service = new ConferenceService(mockContext.Object);

			var conference = GetTestData().First();

			service.CreateItem(conference);
			service.UpdateItem(conference);

			mockDbSet.Verify(m => m.Update(It.IsAny<Conference>()), Times.Once());
		}

		[Fact]
		public void DeleteItemTest()
		{
			var service = new Mock<ConferenceService>(_mockContext.Object);

			var conference = GetTestData().First();

			service.Setup(x => x.DeleteById(conference.Id));
			service.Object.DeleteById(conference.Id);

			service.Verify(i => i.DeleteById(conference.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new Mock<ConferenceService>(_mockContext.Object);

			var conference = GetTestData().First();
			service.Object.CreateItem(conference);

			service.Setup(a => a.Exists(conference.Id));
			service.Object.Exists(conference.Id);
			service.Verify(a => a.Exists(conference.Id));
		}
		
		[Fact]
		public void DoesNotExistTest()
		{
			var service = new Mock<ConferenceService>(_mockContext.Object);

			var guid = Guid.NewGuid();
			service.Setup(a => a.Exists(guid));
			service.Object.Exists(guid);
			service.Verify(a => a.Exists(guid));
		}
		
		[Fact]
		public void GetReportThesesTest()
		{
			var service = new Mock<ConferenceService>(_mockContext.Object);

			var conference = TestData.Conference1;

			service.Setup(it => it.GetReportTheses(conference.Id));
			service.Object.GetReportTheses(conference.Id);
			service.Verify(it => it.GetReportTheses(conference.Id), Times.Once);
		}
		
		[Fact]
		public void GetParticipatorsTest()
		{
			var service = new Mock<ConferenceService>(_mockContext.Object);

			var conference = TestData.Conference1;

			service.Setup(it => it.GetParticipators(conference.Id));
			service.Object.GetParticipators(conference.Id);
			service.Verify(it => it.GetParticipators(conference.Id), Times.Once);
		}
	}
}
