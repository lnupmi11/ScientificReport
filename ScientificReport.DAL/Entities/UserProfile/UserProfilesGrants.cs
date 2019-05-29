using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities.UserProfile
{
	public class UserProfilesGrants
	{
		[Key]
		public Guid Id { get; set; }
		
		public Guid UserProfileId { get; set; }
		public virtual UserProfile UserProfile { get; set; }
		
		public Guid GrantId { get; set; }
		public virtual Grant Grant { get; set; }
	}
}
