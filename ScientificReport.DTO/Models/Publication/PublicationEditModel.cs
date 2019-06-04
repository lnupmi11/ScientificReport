using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Publication
{
	public class PublicationEditModel
	{
		[Required]
		public Guid Id { get; set; }
		
		[Required]
		public DAL.Entities.Publications.Publication.PublicationTypes Type { get; set; }
		
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
		public DAL.Entities.Publications.Publication.PrintStatuses PrintStatus { get; set; }

		public IEnumerable<string> PrintStatusOptions { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }

		public PublicationEditModel()
		{
			Init();
		}

		public PublicationEditModel(DAL.Entities.Publications.Publication publication)
		{
			Init();
			Id = publication.Id;
			Type = publication.PublicationType;
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
			PrintStatus = DAL.Entities.Publications.Publication.PrintStatuses.Any;
			PrintStatusOptions = new[]
			{
				"Any",
				"IsRecommendedToPrint",
				"IsPrintCanceled"
			};
		}
	}
}
