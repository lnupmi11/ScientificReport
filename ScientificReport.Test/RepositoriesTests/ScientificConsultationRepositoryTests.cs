using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class ScientificConsultationRepositoryTests
	{
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
        	var mockContext = new Mock<ScientificReportDbContext>();
        	mockContext.Setup(item => item.ScientificConsultations).Returns(
        		MockProvider.GetMockSet(GetTestData()).Object
        	);
        	return mockContext;
        }

		[Fact]
		public void AllTest()
		{
			var repository = new Mock<ScientificConsultationRepository>(GetMockContext().Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var repository = new ScientificConsultationRepository(GetMockContext().Object);

			var actual = repository.AllWhere(x => x.Id.Equals(TestData.ScientificConsultation1.Id));
			Assert.Single(actual);
		}

		[Fact]
		public void GetByIdTest()
		{
			var repository = new Mock<ScientificConsultationRepository>(GetMockContext().Object);

			var scientificConsultation = GetTestData().First();
			repository.Object.Create(scientificConsultation);

			repository.Setup(item => item.Get(scientificConsultation.Id));
			repository.Object.Get(scientificConsultation.Id);
			repository.Verify(item => item.Get(scientificConsultation.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var repository = new Mock<ScientificConsultationRepository>(GetMockContext().Object);

			var scientificConsultation = GetTestData().First();
			repository.Setup(it => it.Create(scientificConsultation));
			repository.Object.Create(scientificConsultation);
			repository.Verify(it => it.Create(scientificConsultation), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificConsultation>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificConsultations).Returns(mockDbSet.Object);

			var repository = new Mock<ScientificConsultationRepository>(mockContext.Object);

			var scientificConsultation = GetTestData().First();

			repository.Object.Create(scientificConsultation);

			repository.Setup(a => a.Update(scientificConsultation));
			repository.Object.Update(scientificConsultation);
			repository.Verify(a => a.Update(scientificConsultation));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<ScientificConsultation>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.ScientificConsultations).Returns(mockDbSet.Object);

			var repository = new Mock<ScientificConsultationRepository>(mockContext.Object);

			var scientificConsultation = GetTestData().First();

			repository.Setup(x => x.Delete(scientificConsultation.Id));
			repository.Object.Delete(scientificConsultation.Id);
			repository.Verify(i => i.Delete(scientificConsultation.Id));
		}
	}
}
