using System.Collections.Generic;

namespace ScientificReport.DTO.Models.ReportThesis
{
	public class ReportThesisIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.ReportThesis> ReportTheses { get; set; }
	}
}
