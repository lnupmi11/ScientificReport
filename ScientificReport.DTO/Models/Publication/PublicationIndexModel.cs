using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Publication
{
	public class PublicationIndexModel : PageModel
	{
		public int? YearFromFilter { get; set; }
		public int? YearToFilter { get; set; }
		public DAL.Entities.Publications.Publication.PublicationSetType? PublicationSetType { get; set; }
		public DAL.Entities.Publications.Publication.SortByOptions? SortBy { get; set; }
		public IEnumerable<DAL.Entities.Publications.PublicationBase> Publications { get; set; }
		public IEnumerable<string> PublicationSetTypeOptions { get; set; }
		
		public IEnumerable<DAL.Entities.ReportThesis> ReportTheses { get; set; }
		
		public PublicationIndexModel()
		{
			Init();
		}

		public PublicationIndexModel(IEnumerable<DAL.Entities.Publications.Publication> publications)
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
		}
	}
}
