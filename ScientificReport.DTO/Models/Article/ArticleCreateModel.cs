using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Article
{
	public class ArticleCreateModel : ArticleModel
	{
		public DAL.Entities.Article ToArticle()
		{
			return new DAL.Entities.Article
			{
				Type = Type,
				Title = Title,
				Number = Number,
				PagesAmount = PagesAmount,
				DocumentInfo = DocumentInfo,
				IsPeriodical = IsPeriodical,
				LiabilityInfo = LiabilityInfo,
				PublishingYear = PublishingYear,
				PublishingPlace = PublishingPlace,
				IsPrintCanceled = IsPrintCanceled,
				PublishingHouseName = PublishingHouseName,
				IsRecommendedToPrint = IsRecommendedToPrint
			};
		}
	}
}
