using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;
using ScientificReport.BLL.Services;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Repositories;
using Xunit;

namespace ScientificReport.Test.RepositoriesTests
{
	public class PublicationRepositoryTests
	{
		private static IEnumerable<Publication> GetTestData()
		{
			return new[]
			{
				new Publication
				{
					Id = Guid.NewGuid(),
					Type = Publication.Types.Comment,
					Title = "Publication 1",
					Specification = "Specification 1",
					PagesAmount = 10,
					PublishingYear = 2007,
					PublishingPlace = "Publishing Place 1",
					IsPrintCanceled = false,
					PublishingHouseName = "Publishing House Name 1",
					IsRecommendedToPrint = true
				},
				new Publication
				{
					Id = Guid.NewGuid(),
					Type = Publication.Types.Monograph,
					Title = "Publication 2",
					Specification = "Specification 2",
					PagesAmount = 20,
					PublishingYear = 2014,
					PublishingPlace = "Publishing Place 2",
					IsPrintCanceled = true,
					PublishingHouseName = "Publishing House Name 2",
					IsRecommendedToPrint = false
				},
				new Publication
				{
					Id = Guid.NewGuid(),
					Type = Publication.Types.Dictionary,
					Title = "Publication 3",
					Specification = "Specification 3",
					PagesAmount = 30,
					PublishingYear = 2007,
					PublishingPlace = "Publishing Place 3",
					IsPrintCanceled = false,
					PublishingHouseName = "Publishing House Name 3",
					IsRecommendedToPrint = false
				},
			};
		}


		[Fact]
		public void AllTest()
		{
			var list = GetTestData().AsQueryable();
			var mockSet = new Mock<DbSet<Publication>>();
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(mockSet.Object);

			var repository = new Mock<PublicationRepository>(mockContext.Object);

			repository.Setup(a => a.All());
			repository.Object.All();
			repository.Verify(a => a.All());
		}

		[Fact]
		public void AllWhereTest()
		{
			var list = GetTestData().AsQueryable();
			var mockSet = new Mock<DbSet<Publication>>();
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(mockSet.Object);

			var repository = new PublicationRepository(mockContext.Object);

			var actual = repository.AllWhere(u => u.PublishingYear == 2007);

			Assert.Equal(2, actual.Count());
		}

		[Fact]
		public void GetByIdTest()
		{
			var list = GetTestData().AsQueryable();
			var mockSet = new Mock<DbSet<Publication>>();
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(mockSet.Object);

			var publication = GetTestData().First();
			
			var repository = new Mock<PublicationRepository>(mockContext.Object);
			
			repository.Object.Create(publication);
			
			repository.Setup(item => item.Get(publication.Id));
			repository.Object.Get(publication.Id);
			repository.Verify(item => item.Get(publication.Id));
		}

		[Fact]
		public void CreateTest()
		{
			var list = GetTestData().AsQueryable();
			var mockSet = new Mock<DbSet<Publication>>();
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(mockSet.Object);

			var repository = new Mock<PublicationRepository>(mockContext.Object);

			var publication = GetTestData().First();
			
			repository.Setup(it => it.Create(publication));
			repository.Object.Create(publication);
			repository.Verify(it => it.Create(publication), Times.Once);
		}

		[Fact]
		public void UpdateTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var repository = new Mock<PublicationRepository>(mockContext.Object);

			var publication = GetTestData().First();
			
			repository.Object.Create(publication);
			
			repository.Setup(a => a.Update(publication));
			repository.Object.Update(publication);
			repository.Verify(a => a.Update(publication));
		}

		[Fact]
		public void DeleteTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var repository = new Mock<PublicationRepository>(mockContext.Object);

			var publication = GetTestData().First();
			
			repository.Setup(x => x.Delete(publication.Id));
			repository.Object.Delete(publication.Id);
			repository.Verify(i => i.Delete(publication.Id));
		}
	}
}
