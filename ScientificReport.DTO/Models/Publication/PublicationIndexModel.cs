using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Publication
{
	public class PublicationIndexModel
	{
		public int? YearFromFilter { get; set; }
		
		public int? YearToFilter { get; set; }
		
		public DAL.Entities.Publication.PrintStatuses? PrintStatus { get; set; }
		
		public DAL.Entities.Publication.PublicationSetType? PublicationSetType { get; set; }
		
		public DAL.Entities.Publication.SortByOptions? SortBy { get; set; }
		
		public IEnumerable<ScientificReport.DAL.Entities.Publication> Publications { get; set; }
		
		public IEnumerable<string> PrintStatusOptions { get; set; }
		
		public IEnumerable<string> PublicationSetTypeOptions { get; set; }

		public PublicationIndexModel()
		{
			Init();
		}

		public PublicationIndexModel(IEnumerable<DAL.Entities.Publication> publications)
		{
			Init();
			Publications = publications;
		}

		private void Init()
		{
			PublicationSetTypeOptions = new[]
			{
				"Personal",
				"Department",
				"Faculty"
			};
			PrintStatusOptions = new[]
			{
				"Any",
				"IsRecommendedToPrint",
				"IsPrintCanceled"
			};
		}
	}
}
