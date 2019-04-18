using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DAL.Entities
{
	public class PostgraduateDissertationGuidance
	{
		[Key]
		public Guid Id { get; set; }
		
		public virtual UserProfile Guide { get; set; }
		
		public string PostgraduateName { get; set; }
		
		public string Dissertation { get; set; }
		
		public string Speciality { get; set; }
		
		[DataType(DataType.Date)]
		public DateTime DateDegreeGained { get; set; }
		
		public int GraduationYear { get; set; }
	}
}
