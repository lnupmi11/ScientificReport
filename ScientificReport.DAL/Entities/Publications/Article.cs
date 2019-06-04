using System.Collections.Generic;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities.Publications
{
	public class Article : PublicationBase
	{
		public enum ArticleTypes
		{
			ImpactFactor,
			IncludedInWebOfScienceScopusOthers,
			ForeignPublishing,
			ProfessionalPublishingOfUkraine,
			OtherPublishingOfUkraine,
			ReportThesis,
			InternationalReportThesis,
			DomesticReportThesis,
			ForeignReportThesisWithResearchResults
		}
		
		public ArticleTypes ArticleType { get; set; }

		public string LiabilityInfo { get; set; }
		
		public string DocumentInfo { get; set; }
		
		public string PublishingPlace { get; set; }

		public string PublishingHouseName { get; set; }

		public bool IsPeriodical { get; set; }
		
		// For periodical publications only
		public int Number { get; set; }
		
		public int PagesAmount { get; set; }
		
		public bool IsPrintCanceled { get; set; }
		
		public bool IsRecommendedToPrint { get; set; }
		
		public virtual ICollection<UserProfilesArticles> UserProfilesArticles { get; set; }
	}
}
