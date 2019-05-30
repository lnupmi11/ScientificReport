using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Membership
	{
		public enum Types
		{
			EditorialBoard,
			ScientificCouncil,
			ExpertCouncil
		}
		
		[Key]
		public Guid Id { get; set; }
		
		public Types Type { get; set; }
		
		public string MembershipInfo { get; set; }
		
		public virtual UserProfile.UserProfile User { get; set; }
	}
}
