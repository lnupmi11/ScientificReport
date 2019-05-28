using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities
{
	public class Publication
	{
		public enum Types
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
			Type, Title, PublishingHouse, PublishingYear
		}

		[Key]
		public Guid Id { get; set; }
		
		public Types Type { get; set; }
		
		public string Title { get; set; }
		
		public string Specification { get; set; }
		
		public string PublishingPlace { get; set; }
		
		public string PublishingHouseName { get; set; }
		
		public int PublishingYear { get; set; }
		
		public int PagesAmount { get; set; }
		
		public PrintStatuses PrintStatus { get; set; }

		public virtual ICollection<UserProfilesPublications> UserProfilesPublications { get; set; }
	}
}
