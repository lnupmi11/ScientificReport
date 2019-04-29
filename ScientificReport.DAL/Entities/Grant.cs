using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.DAL.Entities
{
	public class Grant
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual ICollection<UserProfilesGrants> UserProfilesGrants { get; set; }
	}
}
