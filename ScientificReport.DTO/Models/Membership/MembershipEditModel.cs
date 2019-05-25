using System;

namespace ScientificReport.DTO.Models.Membership
{
	public class MembershipEditModel : MembershipModel
	{
		public Guid Id { get; set; }

		public MembershipEditModel()
		{
		}

		public MembershipEditModel(DAL.Entities.Membership membership) : base(membership)
		{
			Id = membership.Id;
		}
	}
}
