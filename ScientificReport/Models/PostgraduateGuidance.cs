using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScientificReport.Models
{
	public class PostgraduateGuidance
	{
		[Key]
		public int Id { get; set; }
		
		[ForeignKey("Guide"), Required]
		public virtual UserProfile Guide { get; set; }

		[Required]
		public string PostgraduateName { get; set; }
		
		[Required]
		public string PostgraduateInfo { get; set; }
	}
}
