using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Membership
{
	public class MembershipModel
	{
		[Required]
		public DAL.Entities.Membership.MemberOfChoices MemberOf { get; set; }
		
		[Required]
		public string MembershipInfo { get; set; }

		public DAL.Entities.UserProfile.UserProfile User { get; set; }
		
		public MembershipModel()
		{
		}

		public MembershipModel(DAL.Entities.Membership membership)
		{
			MemberOf = membership.MemberOf;
			MembershipInfo = membership.MembershipInfo;
			User = membership.User;
		}
	}
}
