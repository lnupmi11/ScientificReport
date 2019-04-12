using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Opposition
	{
		[Key]
		public int Id { get; set; }
		
		public string About { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime DateOfOpposition { get; set; }
		
		public virtual UserProfile Opponent { get; set; }
	}
}
