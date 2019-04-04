using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScientificReport.Models
{
	public class ScientificConsultation
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual UserProfile Guide { get; set; }

		[Required]
		public string Name { get; set; }
		
		[Required]
		public string DissertationTitle { get; set; }
	}
}
