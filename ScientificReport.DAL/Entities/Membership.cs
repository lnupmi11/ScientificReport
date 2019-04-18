using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Membership
	{
		public enum MemberOfChoices
		{
			EditorialBoard,
			ScientificCouncil,
			ExpertCouncil
		}
		
		[Key]
		public Guid Id { get; set; }
		
		public MemberOfChoices MemberOf { get; set; }
		
		public string MembershipInfo { get; set; }
	}
}
