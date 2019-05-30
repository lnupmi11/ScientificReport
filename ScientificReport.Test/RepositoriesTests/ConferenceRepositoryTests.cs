using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ConferenceRepositoryTests
	{
		private static readonly IEnumerable<Conference> TestConferences = new[]
		{
			TestData.Conference1,
			TestData.Conference2,
			TestData.Conference3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Conferences).Returns(
				MockProvider.GetMockSet(TestConferences).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new ConferenceRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestConferences.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.Conferences.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			var expected = mockContext.Object.Conferences.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.Conferences.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			var expected = mockContext.Object.Conferences.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			Assert.Equal(TestConferences.Count(), mockContext.Object.Conferences.Count());
			repository.Create(TestData.Conference1);
			Assert.Equal(TestConferences.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			var item = mockContext.Object.Conferences.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new ConferenceRepository(mockContext.Object);
			var item = mockContext.Object.Conferences.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.Conferences.Find(item.Id));
		}
	}
}
