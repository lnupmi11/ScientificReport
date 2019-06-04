namespace ScientificReport.DTO.Models.Article
{
	public class ArticleModel
	{
		public DAL.Entities.Publications.Article.ArticleTypes Type { get; set; }
		
		public string Title { get; set; }
		
		public string LiabilityInfo { get; set; }
		
		public string DocumentInfo { get; set; }
		
		public string PublishingPlace { get; set; }
		
		public string PublishingHouseName { get; set; }
		
		public int PublishingYear { get; set; }
		
		public bool IsPeriodical { get; set; }
		
		// For periodical publications only
		public int Number { get; set; }
		
		public int PagesAmount { get; set; }
		
		public bool IsPrintCanceled { get; set; }
		
		public bool IsRecommendedToPrint { get; set; }
	}
}
