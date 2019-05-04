using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities
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
		public Guid Id { get; set; }
		
		public Types Type { get; set; }
		
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
		
		public virtual ICollection<UserProfilesArticles> UserProfilesArticles { get; set; }
	}
}
