using System;
using System.Collections.Generic;

namespace ScientificReport.DTO.Models.ReportThesis
{
	public class ReportThesisModel
	{
		public string Thesis { get; set; }
		
		public Guid ConferenceId { get; set; }
		
		public DAL.Entities.Conference Conference { get; set; }
		
		public IEnumerable<DAL.Entities.Conference> Conferences { get; set; }

		public ReportThesisModel()
		{
		}
		
		public ReportThesisModel(DAL.Entities.ReportThesis reportThesis)
		{
			Thesis = reportThesis.Thesis;
			Conference = reportThesis.Conference;
		}
	}
}
