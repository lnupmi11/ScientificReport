using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Report
	{
		[Key]
		public Guid Id { get; set; }

		[DataType(DataType.Date)]
		public DateTime Created { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime Edited { get; set; }
	}
}
