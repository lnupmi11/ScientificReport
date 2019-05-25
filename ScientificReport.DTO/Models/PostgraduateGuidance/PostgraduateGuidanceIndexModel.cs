using System.Collections.Generic;

namespace ScientificReport.DTO.Models.PostgraduateGuidance
{
	public class PostgraduateGuidanceIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.PostgraduateGuidance> PostgraduateGuidances { get; set; }
	}
}
