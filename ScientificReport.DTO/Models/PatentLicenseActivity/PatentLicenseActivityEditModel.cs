using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.PatentLicenseActivity
{
	public class PatentLicenseActivityEditModel : PatentLicenseActivityModel
	{
	    [Required]
		public Guid Id { get; set; }

		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> AuthorsOrApplicants { get; set; }

		public PatentLicenseActivityEditModel()
        {
        }

        public PatentLicenseActivityEditModel(DAL.Entities.PatentLicenseActivity patentLicenseActivity) : base(patentLicenseActivity)
        {
        	Id = patentLicenseActivity.Id;
        }
	}
}
