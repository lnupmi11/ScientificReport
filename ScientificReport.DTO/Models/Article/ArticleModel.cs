using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Article
{
	public class ArticleModel
	{
		[Required]
		public DAL.Entities.Article.Types Type { get; set; }
		
		[Required]
		public string Title { get; set; }
		
		[Required]
		public string LiabilityInfo { get; set; }
		
		[Required]
		public string DocumentInfo { get; set; }
		
		[Required]
		public string PublishingPlace { get; set; }
		
		[Required]
		public string PublishingHouseName { get; set; }
		
		[Required]
		public int PublishingYear { get; set; }
		
		[Required]
		public bool IsPeriodical { get; set; }
		
		// For periodical publications only
		public int Number { get; set; }
		
		[Required]
		public int PagesAmount { get; set; }
		
		[Required]
		public bool IsPrintCanceled { get; set; }
		
		[Required]
		public bool IsRecommendedToPrint { get; set; }
	}
}
