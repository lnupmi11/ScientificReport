using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities.UserProfile
{
	public class UserProfilesPublications
	{
		[Key]
		public Guid Id { get; set; }
		
		public Guid UserProfileId { get; set; }
		public virtual UserProfile UserProfile { get; set; }
		
		public Guid PublicationId { get; set; }
		public virtual Publication Publication { get; set; }
	}
}
