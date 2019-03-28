using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Publication
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string Title { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Authors { get; set; }
		
		[Required]
		public string City { get; set; }
		
		[Required]
		public int Year { get; set; }
		
		[Required]
		public int Volume { get; set; }
		
		[Required]
		public string Type { get; set; }
		
		public bool IsFinished { get; set; }
		
		public bool IsRecommendedToPrint { get; set; }
	}
}
