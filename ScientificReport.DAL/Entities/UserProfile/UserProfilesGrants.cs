using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class UserProfilesGrants
	{
		[Key]
		public int Id { get; set; }
		
		public int UserProfileId { get; set; }
		public virtual UserProfile UserProfile { get; set; }
		
		public int GrantId { get; set; }
		public virtual Grant Grant { get; set; }
	}
}
