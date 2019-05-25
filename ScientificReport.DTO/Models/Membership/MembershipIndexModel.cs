using System.Collections.Generic;

namespace ScientificReport.DTO.Models.Membership
{
	public class MembershipIndexModel : PageModel
	{
		public IEnumerable<DAL.Entities.Membership> Memberships { get; set; }
	}
}
