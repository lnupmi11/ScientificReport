using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.ScientificInternship
{
	public class ScientificInternshipEditModel : ScientificInternshipModel
	{
	    [Required]
		public Guid Id { get; set; }

		public IEnumerable<DAL.Entities.UserProfile.UserProfile> AllUsers { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }

		public ScientificInternshipEditModel()
        {
        }

        public ScientificInternshipEditModel(DAL.Entities.ScientificInternship scientificInternship) : base(scientificInternship)
        {
        	Id = scientificInternship.Id;
        }
	}
}
