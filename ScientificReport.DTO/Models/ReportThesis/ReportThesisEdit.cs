using System.Collections;
using System.Collections.Generic;
using ScientificReport.DAL.Entities;

namespace ScientificReport.DTO.Models.ReportThesis
{
	public class ReportThesisEdit
	{
		public DAL.Entities.ReportThesis ReportThesis { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
	}
}