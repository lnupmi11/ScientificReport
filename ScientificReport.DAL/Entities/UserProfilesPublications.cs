using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class UserProfilesPublications
	{
		[Key]
		public int Id { get; set; }
		
		public string UserProfileId { get; set; }
		public virtual UserProfile UserProfile { get; set; }
		
		public int PublicationId { get; set; }
		public virtual Publication Publication { get; set; }
	}
}
