using System;

namespace ScientificReport.DTO.Models.PatentLicenseActivity
{
	public class PatentLicenseActivityUpdUserRequest
	{
		public Guid UserId { get; set; }
		
		public DAL.Entities.PatentLicenseActivity.Types ActivityType { get; set; }
	}
}
