using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.DTO.Models.ScientificWorks
{
	public class ScientificWorkIndexModel : PageModel
	{
		public IEnumerable<ScientificWork> ScientificWorks { get; set; }
	}
}
