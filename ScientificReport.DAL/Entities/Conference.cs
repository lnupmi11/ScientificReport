using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Conference
	{
		public enum Types
		{
			Local,
			Foreign,
		}

		[Key]
		public Guid Id { get; set; }
		
		public string Topic { get; set; }
		
		public Types Type { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime Date { get; set; }
	}
}
