using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Article
{
	public class ArticleEditModel : ArticleModel
	{
		[Required]
		public Guid Id { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users;
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors;

		public ArticleEditModel()
		{
		}
		
		public ArticleEditModel(DAL.Entities.Article article)
		{
			Id = article.Id;
			Type = article.ArticleType;
			Title = article.Title;
			PagesAmount = article.PagesAmount;
			DocumentInfo = article.DocumentInfo;
			IsPeriodical = article.IsPeriodical;
			LiabilityInfo = article.LiabilityInfo;
			PublishingYear = article.PublishingYear;
			PublishingPlace = article.PublishingPlace;
			IsPrintCanceled = article.IsPrintCanceled;
			PublishingHouseName = article.PublishingHouseName;
			IsRecommendedToPrint = article.IsRecommendedToPrint;
		}

		public DAL.Entities.Article Modify(DAL.Entities.Article article)
		{
			article.ArticleType = Type;
			article.Title = Title;
			article.PagesAmount = PagesAmount;
			article.DocumentInfo = DocumentInfo;
			article.IsPeriodical = IsPeriodical;
			article.LiabilityInfo = LiabilityInfo;
			article.PublishingYear = PublishingYear;
			article.PublishingPlace = PublishingPlace;
			article.IsPrintCanceled = IsPrintCanceled;
			article.PublishingHouseName = PublishingHouseName;
			article.IsRecommendedToPrint = IsRecommendedToPrint;
			return article;
		}
	}
}
