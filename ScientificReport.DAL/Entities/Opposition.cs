using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class Opposition
	{
		[Key]
		public Guid Id { get; set; }
		
		public string About { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime DateOfOpposition { get; set; }
		
		public virtual UserProfile.UserProfile Opponent { get; set; }
	}
}
