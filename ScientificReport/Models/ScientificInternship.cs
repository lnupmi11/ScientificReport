using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class ScientificInternship
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string PlaceOfInternship { get; set; }
		
		[Required, DataType(DataType.Date)]
		public DateTime Started { get; set; }
		
		[Required, DataType(DataType.Date)]
		public DateTime Ended { get; set; }

		[Required]
		public string Contents { get; set; }
		
		[Required]
		public virtual ICollection<UserProfile> Users { get; set; }
	}
}
