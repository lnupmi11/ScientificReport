using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Article
	{
		public enum Types
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
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		public int Type { get; set; }
		
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
		
		public bool IsPeriodical { get; set; }
		
		// For periodical publications only
		public int Number { get; set; }
		
		[Required]
		public int PagesAmount { get; set; }
		
		public bool IsPrintCanceled { get; set; }
		
		public bool IsRecommendedToPrint { get; set; }
		
		public virtual ICollection<UserProfilesArticles> UserProfilesArticles { get; set; }
	}
}
