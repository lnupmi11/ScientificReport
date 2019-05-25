using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.ScientificConsultation
{
	public class ScientificConsultationEditModel : ScientificConsultationModel
	{
	    [Required]
		public Guid Id { get; set; }

		public ScientificConsultationEditModel()
        {
        }

        public ScientificConsultationEditModel(DAL.Entities.ScientificConsultation scientificConsultation) : base(scientificConsultation)
        {
        	Id = scientificConsultation.Id;
        }
	}
}
