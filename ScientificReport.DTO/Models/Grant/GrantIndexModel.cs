using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Grant
{
	public class GrantIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.Grant> Grants { get; set; }
	}
}
