using System.Collections.Generic;

namespace ScientificReport.DTO.Models.ScientificInternship
{
	public class ScientificInternshipIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.ScientificInternship> ScientificInternships { get; set; }
	}
}
