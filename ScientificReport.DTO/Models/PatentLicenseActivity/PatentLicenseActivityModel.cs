using System;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.PatentLicenseActivity
{
	public class PatentLicenseActivityModel
	{
		[Required]
		public string Name { get; set; }
		
		[Required]
		public int Number { get; set; }
		
		[Required]
		[DataType(DataType.DateTime)]
		public DateTime DateTime { get; set; }
		
		[Required]
		public DAL.Entities.PatentLicenseActivity.Types Type { get; set; }
		
	    public PatentLicenseActivityModel()
    	{
    	}

        public PatentLicenseActivityModel(DAL.Entities.PatentLicenseActivity patentLicenseActivity)
        {
	        Name = patentLicenseActivity.Name;
	        Number = patentLicenseActivity.Number;
	        DateTime = patentLicenseActivity.DateTime;
	        Type = patentLicenseActivity.Type;
        }
	}
}
