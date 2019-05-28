using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.PatentLicenseActivity
{
	public class PatentLicenseActivityEditModel : PatentLicenseActivityModel
	{
	    [Required]
		public Guid Id { get; set; }

		public PatentLicenseActivityEditModel()
        {
        }

        public PatentLicenseActivityEditModel(DAL.Entities.PatentLicenseActivity patentLicenseActivity) : base(patentLicenseActivity)
        {
        	Id = patentLicenseActivity.Id;
        }
	}
}
