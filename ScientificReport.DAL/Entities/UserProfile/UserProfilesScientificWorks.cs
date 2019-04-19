using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class UserProfilesScientificWorks
	{
		[Key]
		public int Id { get; set; }
		
		public virtual UserProfile UserProfile { get; set; }
		
		public virtual ScientificWork ScientificWork { get; set; }
	}
}
