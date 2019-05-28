using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.DTO.Models.ReportThesis
{
	public class ReportThesisEdit : ReportThesisModel
	{
		[Required]
		public Guid Id { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Authors { get; set; }
		
		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }

		public ReportThesisEdit()
		{
		}
		
		public ReportThesisEdit(DAL.Entities.ReportThesis reportThesis) : base(reportThesis)
		{
			Id = reportThesis.Id;
		}
	}
}
