using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Review
{
	public class ReviewIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.Review> Reviews { get; set; }
	}
}
