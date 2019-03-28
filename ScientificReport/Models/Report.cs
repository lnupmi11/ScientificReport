using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class Report
	{
		public int Id { get; set; }

		[DataType(DataType.Date)]
		public DateTime Created { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime Edited { get; set; }
	}
}
