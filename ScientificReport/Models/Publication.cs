using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Publication
	{
		public enum Types
		{
			Monograph, TextBook, HandBook, Dictionary, Translation, Comment, BibliographicIndex, Other
		}
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		public int Type { get; set; }
		
		[Required]
		public virtual ICollection<UserProfilesPublications> UserProfilesPublications { get; set; }
		
		[Required]
		public string Title { get; set; }
		
		[Required]
		public string Specification { get; set; }
		
		[Required]
		public string PublishingPlace { get; set; }
		
		[Required]
		public string PublishingHouseName { get; set; }
		
		[Required]
		public int PublishingYear { get; set; }
		
		[Required]
		public int PagesAmount { get; set; }
		
		public bool IsPrintCanceled { get; set; }
		
		public bool IsRecommendedToPrint { get; set; }
	}
}
