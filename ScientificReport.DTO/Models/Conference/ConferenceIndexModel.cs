using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Conference
{
	public class ConferenceIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.Conference> Conferences { get; set; }
	}
}
