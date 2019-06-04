using System.Collections.Generic;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities.Publications
{
	public class Publication : PublicationBase
	{
		public enum PublicationTypes
		{
			Monograph, TextBook, HandBook, Dictionary, Translation, Comment, BibliographicIndex, Other
		}

		public enum PrintStatuses
		{
			IsPrintCanceled, IsRecommendedToPrint, Any
		}
		
		public enum PublicationSetType
		{
			Personal, Department, Faculty
		}
		
		public enum SortByOptions
		{
			Title
		}

		public PublicationTypes PublicationType { get; set; }

		public string Specification { get; set; }
		
		public string PublishingPlace { get; set; }

		public string PublishingHouseName { get; set; }

		public int PagesAmount { get; set; }
		
		public PrintStatuses PrintStatus { get; set; }

		public virtual ICollection<UserProfilesPublications> UserProfilesPublications { get; set; }
	}
}
