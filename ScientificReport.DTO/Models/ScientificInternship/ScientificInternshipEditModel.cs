using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.ScientificInternship
{
	public class ScientificInternshipEditModel : ScientificInternshipModel
	{
	    [Required]
		public Guid Id { get; set; }

		public ScientificInternshipEditModel()
        {
        }

        public ScientificInternshipEditModel(DAL.Entities.ScientificInternship scientificInternship) : base(scientificInternship)
        {
        	Id = scientificInternship.Id;
        }
	}
}
