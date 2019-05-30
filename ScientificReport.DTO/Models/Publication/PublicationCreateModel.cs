using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Publication
{
	public class PublicationCreateModel
	{
		[Required]
		public Guid Id { get; set; }
		
		[Required]
		public DAL.Entities.Publication.Types Type { get; set; }
		
		[Required]
		public string Title { get; set; }
		
		[Required]
		public string Specification { get; set; }
		
		[Required]
		public string PublishingPlace { get; set; }
		
		[Required]
		public string PublishingHouseName { get; set; }
		
		[Required]
		public int PublishingYear { get; set; }
		
		[Required]
		public int PagesAmount { get; set; }
		
		[Required]
		public DAL.Entities.Publication.PrintStatuses PrintStatus { get; set; }

		public IEnumerable<string> PrintStatusOptions { get; set; }

		public PublicationCreateModel()
		{
			Init();
		}

		public PublicationCreateModel(DAL.Entities.Publication publication)
		{
			Init();
			Type = publication.Type;
			Title = publication.Title;
			Specification = publication.Specification;
			PublishingPlace = publication.PublishingPlace;
			PublishingHouseName = publication.PublishingHouseName;
			PublishingYear = publication.PublishingYear;
			PagesAmount = publication.PagesAmount;
			PrintStatus = publication.PrintStatus;
		}

		private void Init()
		{
			PrintStatus = DAL.Entities.Publication.PrintStatuses.Any;
			PrintStatusOptions = new[]
			{
				"Any",
				"IsRecommendedToPrint",
				"IsPrintCanceled"
			};
		}

		public DAL.Entities.Publication ToPublication()
		{
			return new DAL.Entities.Publication
			{
				Type = Type,
				Title = Title,
				Specification = Specification,
				PublishingPlace = PublishingPlace,
				PublishingHouseName = PublishingHouseName,
				PublishingYear = PublishingYear,
				PagesAmount = PagesAmount,
				PrintStatus = PrintStatus
			};
		}
	}
}
