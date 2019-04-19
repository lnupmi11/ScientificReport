using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Conference
	{
		[Key]
		public Guid Id { get; set; }
		
		public string Topic { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
	}
}
