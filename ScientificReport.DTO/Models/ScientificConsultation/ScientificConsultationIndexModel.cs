using System.Collections.Generic;

namespace ScientificReport.DTO.Models.ScientificConsultation
{
	public class ScientificConsultationIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.ScientificConsultation> ScientificConsultations { get; set; }
	}
}
