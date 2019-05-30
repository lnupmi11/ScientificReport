using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Membership
{
	public class MembershipModel
	{
		[Required]
		public DAL.Entities.Membership.Types MemberOf { get; set; }
		
		[Required]
		public string MembershipInfo { get; set; }
		
		public MembershipModel()
		{
		}

		public MembershipModel(DAL.Entities.Membership membership)
		{
			MemberOf = membership.Type;
			MembershipInfo = membership.MembershipInfo;
		}
	}
}
