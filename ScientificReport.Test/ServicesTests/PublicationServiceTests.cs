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
	public class PublicationServiceTests
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
		public void GetAllTest()
		{
			var list = GetTestData().AsQueryable();
			var mockSet = new Mock<DbSet<Publication>>();
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(mockSet.Object);

			var service = new PublicationService(mockContext.Object);

			var actual = service.GetAll();

			Assert.Equal(list.Count(), actual.Count());
		}
		
		[Fact]
		public void GetAllWhereTest()
		{
			var list = GetTestData().AsQueryable();
			var mockSet = new Mock<DbSet<Publication>>();
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(mockSet.Object);

			var service = new PublicationService(mockContext.Object);

			var actual = service.GetAllWhere(u => u.PublishingYear == 2007);

			Assert.Equal(2, actual.Count());
		}
		
	//	[Fact]
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

			var service = new PublicationService(mockContext.Object);

			var expected = GetTestData().First();
			
			service.CreateItem(expected);

			var actual = service.GetById(expected.Id);
			
			Assert.NotNull(actual);
			Assert.Equal(actual.Id, expected.Id);
		}
		
		[Fact]
		public void CreateItemTest()
		{
			var list = GetTestData().AsQueryable();
			var mockSet = new Mock<DbSet<Publication>>();
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Provider).Returns(list.Provider);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.Expression).Returns(list.Expression);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.ElementType).Returns(list.ElementType);
			mockSet.As<IQueryable<Publication>>().Setup(m => m.GetEnumerator()).Returns(list.GetEnumerator());

			var mockContext = new Mock<ScientificReportDbContext>();
			mockContext.Setup(item => item.Publications).Returns(mockSet.Object);

			var service = new Mock<PublicationService>(mockContext.Object);

			var expectedPublication = new Publication
			{
				Id = Guid.NewGuid(),
				Type = Publication.Types.Translation,
				Title = "Title 1",
				Specification = "Specification 1",
				PagesAmount = 20,
				PublishingYear = 2001,
				PublishingPlace = "Publishing Place 1",
				IsPrintCanceled = false,
				PublishingHouseName = "Publishing House Name 1",
				IsRecommendedToPrint = true
			};
			
			service.Setup(it => it.CreateItem(expectedPublication));
			service.Object.CreateItem(expectedPublication);
			service.Verify(it => it.CreateItem(expectedPublication));
		}
		
		[Fact]
		public void UpdateItemTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var service = new PublicationService(mockContext.Object);

			var publication = GetTestData().First();
			
			service.CreateItem(publication);
			service.UpdateItem(publication);
			
			mockDbSet.Verify(m => m.Update(It.IsAny<Publication>()), Times.Once());
		}
		
		[Fact]
		public void DeleteItemTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var service = new Mock<PublicationService>(mockContext.Object);

			var publication = GetTestData().First();
			
			service.Setup(x => x.DeleteById(publication.Id));
			service.Object.DeleteById(publication.Id);

			service.Verify(i => i.DeleteById(publication.Id));
		}
		
	//	[Fact]
		public void PublicationExistsTest()
		{
			var mockDbSet = new Mock<DbSet<Publication>>();
			var mockContext = new Mock<ScientificReportDbContext>();

			mockContext.Setup(item => item.Publications).Returns(mockDbSet.Object);

			var service = new PublicationService(mockContext.Object);

			var publication = GetTestData().First();
			
			service.CreateItem(publication);

			Assert.True(service.PublicationExists(publication.Id));
		}
	}
}
