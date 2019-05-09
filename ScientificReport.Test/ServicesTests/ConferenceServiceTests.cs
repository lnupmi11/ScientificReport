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
		private readonly Mock<DbSet<Conference>> _mockDbSet = MockProvider.GetMockSet(GetTestData().AsQueryable());

		private static IEnumerable<Conference> GetTestData()
		{
			return new[]
			{
				TestData.Conference1,
				TestData.Conference2
			};
		}

		private Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Conferences).Returns(_mockDbSet.Object);
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
			var service = new ConferenceService(GetMockContext().Object);
			var actual = service.GetAllWhere(u => u.Id.Equals(TestData.Conference1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var expected = GetTestData().First();
			var service = new ConferenceService(GetMockContext().Object);

			var actual = service.GetById(expected.Id);

			Assert.NotNull(actual);
			Assert.Equal(expected.Id, actual.Id);
		}

		[Fact]
		public void CreateItemTest()
		{
			var service = new ConferenceService(GetMockContext().Object);

			var expected = TestData.Conference3;
			service.CreateItem(expected);

			_mockDbSet.Verify(m => m.Add(It.IsAny<Conference>()), Times.Once);
		}

		[Fact]
		public void UpdateItemTest()
		{
			var service = new ConferenceService(GetMockContext().Object);

			var expected = GetTestData().First();
			expected.Topic = TestData.Conference3.Topic;
			service.UpdateItem(expected);

			_mockDbSet.Verify(m => m.Update(expected), Times.Once);
		}

		[Fact]
		public void DeleteItemTest()
		{
			var mockContext = GetMockContext();
			var service = new ConferenceService(mockContext.Object);

			var item = mockContext.Object.Conferences.First();

			Assert.True(service.Exists(item.Id));

			service.DeleteById(item.Id);

			Assert.False(service.Exists(item.Id));
		}

		[Fact]
		public void ExistsTest()
		{
			var service = new ConferenceService(GetMockContext().Object);

			var item = GetTestData().First();
			var exists = service.Exists(item.Id);

			Assert.True(exists);
		}

		[Fact]
		public void DoesNotExistTest()
		{
			var service = new ConferenceService(GetMockContext().Object);

			var item = TestData.Conference3;
			var exists = service.Exists(item.Id);

			Assert.False(exists);
		}
		
		[Fact]
		public void GetReportThesesTest()
		{
			var service = new Mock<ConferenceService>(GetMockContext().Object);

			var conference = TestData.Conference1;

			service.Setup(it => it.GetReportTheses(conference.Id));
			service.Object.GetReportTheses(conference.Id);
			service.Verify(it => it.GetReportTheses(conference.Id), Times.Once);
		}
		
		[Fact]
		public void GetParticipatorsTest()
		{
			var service = new Mock<ConferenceService>(GetMockContext().Object);

			var conference = TestData.Conference1;

			service.Setup(it => it.GetParticipators(conference.Id));
			service.Object.GetParticipators(conference.Id);
			service.Verify(it => it.GetParticipators(conference.Id), Times.Once);
		}
	}
}
