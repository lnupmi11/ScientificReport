using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Membership
	{
		public enum MemberOfChoices
		{
			// Редколегія
			EditorialBoard,
			
			// Вчена рада
			ScientificCouncil,
			
			// Експертна рада
			ExpertCouncil
		}
		
		[Key]
		public int Id { get; set; }
		
		[Required]
		public MemberOfChoices MemberOf { get; set; }
		
		[Required]
		public string MembershipInfo { get; set; }
	}
}
