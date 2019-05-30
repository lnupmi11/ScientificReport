using System.Collections.Generic;
using System.Linq;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ScientificConsultationRepositoryTests
	{
		private static readonly IEnumerable<ScientificConsultation> TestScientificConsultations = new[]
		{
			TestData.ScientificConsultation1,
			TestData.ScientificConsultation2,
			TestData.ScientificConsultation3
		};

		private static Mock<ScientificReportDbContext> GetMockContext()
		{
			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.ScientificConsultations).Returns(
				MockProvider.GetMockSet(TestScientificConsultations).Object
			);
			return mockContext;
		}

		[Fact]
		public void AllTest()
		{
			var repository = new ScientificConsultationRepository(GetMockContext().Object);
			var actual = repository.All();
			Assert.Equal(TestScientificConsultations.Count(), actual.Count());
		}

		[Fact]
		public void AllWhereTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			var actual = repository.AllWhere(a => a.Id == mockContext.Object.ScientificConsultations.First().Id);
			Assert.Single(actual);
		}
		
		[Fact]
		public void GetTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			var expected = mockContext.Object.ScientificConsultations.First();
			var actual = repository.Get(o => o.Id == expected.Id);
			Assert.NotNull(actual);
		}
		
		[Fact]
		public void GetQueryTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			var actual = repository.GetQuery();
			Assert.Equal(actual.Count(), mockContext.Object.ScientificConsultations.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			var expected = mockContext.Object.ScientificConsultations.First();
			var actual = repository.Get(expected.Id);
			Assert.NotNull(actual);
		}

		[Fact]
		public void CreateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			Assert.Equal(TestScientificConsultations.Count(), mockContext.Object.ScientificConsultations.Count());
			repository.Create(TestData.ScientificConsultation1);
			Assert.Equal(TestScientificConsultations.Count(), repository.All().Count());
		}

		[Fact]
		public void UpdateTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			var item = mockContext.Object.ScientificConsultations.First();
			repository.Update(item);
			Assert.NotNull(repository.Get(item.Id));
		}
		
		[Fact]
		public void UpdateItemIsNullTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			repository.Update(null);
		}

		[Fact]
		public void DeleteTest()
		{
			var mockContext = GetMockContext();
			var repository = new ScientificConsultationRepository(mockContext.Object);
			var item = mockContext.Object.ScientificConsultations.First();
			repository.Delete(item.Id);
			Assert.Null(mockContext.Object.ScientificConsultations.Find(item.Id));
		}
	}
}
