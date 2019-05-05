using System.Collections.Generic;

namespace ScientificReport.DTO.Models.ReportThesis
{
	public class ReportThesisDetails
	{
		public DAL.Entities.ReportThesis ReportThesis { get; set; }
		public ICollection<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
	}
}