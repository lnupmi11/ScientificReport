using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class ScientificConsultation
	{
		[Key]
		public int Id { get; set; }
		
		public virtual UserProfile Guide { get; set; }

		public string Name { get; set; }
		
		public string DissertationTitle { get; set; }
	}
}