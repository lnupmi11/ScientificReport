using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Conference
	{
		[Key]
		public int Id { get; set; }
		
		[Required]
		public string Topic { get; set; }
		
		[Required, DataType(DataType.Date)]
		public DateTime Date { get; set; }
	}
}
