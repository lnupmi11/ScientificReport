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
		public string Id { get; set; }
		
		public int Type { get; set; }
		
		public string Title { get; set; }
		
		public string Specification { get; set; }
		
		public string PublishingPlace { get; set; }
		
		public string PublishingHouseName { get; set; }
		
		public int PublishingYear { get; set; }
		
		public int PagesAmount { get; set; }
		
		public bool IsPrintCanceled { get; set; }
		
		public bool IsRecommendedToPrint { get; set; }
		
		public virtual ICollection<UserProfilesPublications> UserProfilesPublications { get; set; }
	}
}
