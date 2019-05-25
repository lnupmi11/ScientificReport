using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.Opposition
{
	public class OppositionModel
	{
		[Required]
		public string About { get; set; }
		
		[Required]
		[DataType(DataType.Date)]
		public DateTime DateOfOpposition { get; set; }
		
		[Required]
		public DAL.Entities.UserProfile.UserProfile Opponent { get; set; }

		public OppositionModel()
		{
		}

		public OppositionModel(DAL.Entities.Opposition opposition)
		{
			About = opposition.About;
			DateOfOpposition = opposition.DateOfOpposition;
			Opponent = opposition.Opponent;
		}
	}
}
