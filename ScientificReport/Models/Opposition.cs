using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Opposition
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string About { get; set; }
		
		[Required, DataType(DataType.Date)]
		public DateTime DateOfOpposition { get; set; }
		
		[Required]
		public virtual UserProfile Opponent { get; set; }
	}
}
