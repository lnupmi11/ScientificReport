using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class ParticipationInScientificSitting
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Participators { get; set; }
	}
}
