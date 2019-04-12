using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class PostgraduateGuidance
	{
		[Key]
		public int Id { get; set; }
		
		public virtual UserProfile Guide { get; set; }

		public string PostgraduateName { get; set; }
		
		public string PostgraduateInfo { get; set; }
	}
}
