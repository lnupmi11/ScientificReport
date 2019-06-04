using System.Collections.Generic;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DTO.Models.ScientificWorks
{
	public class ScientificWorkIndexModel : PageModel
	{
		public IEnumerable<ScientificWork> ScientificWorks { get; set; }
	}
}
