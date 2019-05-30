using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Opposition
{
	public class OppositionIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.Opposition> Oppositions { get; set; }
	}
}
