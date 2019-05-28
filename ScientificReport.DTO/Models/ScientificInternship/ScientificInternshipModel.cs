using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.ScientificInternship
{
	public class ScientificInternshipModel
	{
		[Required]
		public string PlaceOfInternship { get; set; }
		
		[Required]
		[DataType(DataType.Date)]
		public DateTime Started { get; set; }
		
		[Required]
		[DataType(DataType.Date)]
		public DateTime Ended { get; set; }

		[Required]
		public string Contents { get; set; }
		
		public ScientificInternshipModel()
		{
		}

		public ScientificInternshipModel(DAL.Entities.ScientificInternship scientificInternship)
		{
			PlaceOfInternship = scientificInternship.PlaceOfInternship;
			Started = scientificInternship.Started;
			Ended = scientificInternship.Ended;
			Contents = scientificInternship.Contents;
		}
	}
}
