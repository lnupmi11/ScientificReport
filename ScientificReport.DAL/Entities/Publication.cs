using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Publication
	{
		public enum Types
		{
			Monograph, TextBook, HandBook, Dictionary, Translation, Comment, BibliographicIndex, Other
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
		
		public bool IsPrintCanceled { get; set; }
		
		public bool IsRecommendedToPrint { get; set; }
		
		public virtual ICollection<UserProfilesPublications> UserProfilesPublications { get; set; }
		
		public DateTime CreatedAt { get; set; }
		
		public virtual UserProfile CreatedBy { get; set; }
		
		public DateTime LastEditAt { get; set; }
		
		public virtual UserProfile LastEditBy { get; set; }
	}
}
