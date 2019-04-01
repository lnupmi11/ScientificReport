using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ScientificReport.Models
{
	public class PostgraduateDissertationGuidance
	{
		[Key]
		public int Id { get; set; }
		
		[ForeignKey("Guide"), Required]
		public virtual UserProfile Guide { get; set; }
		
		[Required]
		public string PostgraduateName { get; set; }
		
		[Required]
		public string Dissertation { get; set; }
		
		[Required]
		public string Speciality { get; set; }
		
		[Required, DataType(DataType.Date)]
		public DateTime DateDegreeGained { get; set; }
		
		[Required]
		public int GraduationYear { get; set; }
	}
}
