using System.Collections.Generic;

namespace ScientificReport.DTO.Models.PostgraduateDissertationGuidance
{
	public class PostgraduateDissertationGuidanceIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }
	}
}
