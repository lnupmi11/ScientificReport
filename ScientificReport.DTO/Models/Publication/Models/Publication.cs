using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Publication.Models
{
	public class Publication
	{
		public DAL.Entities.Publications.Publication.PublicationTypes Type { get; set; }
		
		public string Title { get; set; }
		
		public string Specification { get; set; }
		
		public string PublishingPlace { get; set; }
		
		public string PublishingHouseName { get; set; }
		
		public int PublishingYear { get; set; }
		
		public int PagesAmount { get; set; }
		
		public DAL.Entities.Publications.Publication.PrintStatuses PrintStatus { get; set; }

		public IEnumerable<string> PrintStatusOptions { get; set; }
	}
}
