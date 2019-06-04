using ScientificReport.DAL.Entities;
using ScientificReport.DTO.Models.Article;
using ScientificReport.DTO.Models.ReportThesis;

namespace ScientificReport.DTO.Models.Publication
{
	public class PublicationCreateModel
	{
		public Models.Publication Publication { get; set; }
		
		public ArticleModel Article { get; set; }
		
		public ScientificWork ScientificWork { get; set; }
		
		public ReportThesisModel ReportThesis { get; set; }

		public PublicationTypes PublicationType { get; set; }
		
		public enum PublicationTypes
		{
			Article, ScientificWork, ReportThesis, Other
		}

		public PublicationCreateModel()
		{
			Init();
		}

		public PublicationCreateModel(DAL.Entities.Publication publication)
		{
			Init();
			Publication.Type = publication.PublicationType;
			Publication.Title = publication.Title;
			Publication.Specification = publication.Specification;
			Publication.PublishingPlace = publication.PublishingPlace;
			Publication.PublishingHouseName = publication.PublishingHouseName;
			Publication.PublishingYear = publication.PublishingYear;
			Publication.PagesAmount = publication.PagesAmount;
			Publication.PrintStatus = publication.PrintStatus;
		}

		private void Init()
		{
			Publication = new Models.Publication
			{
				PrintStatus = DAL.Entities.Publication.PrintStatuses.Any,
				PrintStatusOptions = new[] {"Any", "IsRecommendedToPrint", "IsPrintCanceled"}
			};
			Article = new ArticleModel();
			ReportThesis = new ReportThesisModel();
			ScientificWork = new ScientificWork();
		}

		public DAL.Entities.Publication ToPublication()
		{
			return new DAL.Entities.Publication
			{
				PublicationType = Publication.Type,
				Title = Publication.Title,
				Specification = Publication.Specification,
				PublishingPlace = Publication.PublishingPlace,
				PublishingHouseName = Publication.PublishingHouseName,
				PublishingYear = Publication.PublishingYear,
				PagesAmount = Publication.PagesAmount,
				PrintStatus = Publication.PrintStatus
			};
		}
		
		public DAL.Entities.Article ToArticle()
		{
			return new DAL.Entities.Article
			{
				ArticleType = Article.Type,
				Title = Article.Title,
				Number = Article.Number,
				PagesAmount = Article.PagesAmount,
				DocumentInfo = Article.DocumentInfo,
				IsPeriodical = Article.IsPeriodical,
				LiabilityInfo = Article.LiabilityInfo,
				PublishingYear = Article.PublishingYear,
				PublishingPlace = Article.PublishingPlace,
				IsPrintCanceled = Article.IsPrintCanceled,
				PublishingHouseName = Article.PublishingHouseName,
				IsRecommendedToPrint = Article.IsRecommendedToPrint
			};
		}
	}
}
