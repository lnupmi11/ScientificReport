using System.Collections.Generic;

namespace ScientificReport.DTO.Models.PatentLicenseActivity
{
	public class PatentLicenseActivityIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.PatentLicenseActivity> PatentLicenseActivities { get; set; }
	}
}
