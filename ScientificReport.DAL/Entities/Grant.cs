using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Grant
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual ICollection<UserProfilesGrants> UserProfilesGrants { get; set; }
	}
}
