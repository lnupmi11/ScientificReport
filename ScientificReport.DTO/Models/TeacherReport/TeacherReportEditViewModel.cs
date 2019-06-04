using System.Collections.Generic;

namespace ScientificReport.DTO.Models.TeacherReport
{
	public class TeacherReportEditViewModel
	{
		public DAL.Entities.Reports.TeacherReport TeacherReport { get; set; }

		public IEnumerable<DAL.Entities.UserProfile.UserProfile> Users { get; set; }
		public IEnumerable<DAL.Entities.Publications.Publication> Publications { get; set; }
		public IEnumerable<DAL.Entities.Publications.Article> Articles { get; set; }
		public IEnumerable<DAL.Entities.Publications.ScientificWork> ScientificWorks { get; set; }
		public IEnumerable<DAL.Entities.ReportThesis> ReportTheses { get; set; }
		public IEnumerable<DAL.Entities.Grant> Grants { get; set; }
		public IEnumerable<DAL.Entities.ScientificInternship> ScientificInternships { get; set; }
		public IEnumerable<DAL.Entities.PostgraduateGuidance> PostgraduateGuidances { get; set; }
		public IEnumerable<DAL.Entities.ScientificConsultation> ScientificConsultations { get; set; }
		public IEnumerable<DAL.Entities.PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }
		public IEnumerable<DAL.Entities.Review> Reviews { get; set; }
		public IEnumerable<DAL.Entities.Opposition> Oppositions { get; set; }
		public IEnumerable<DAL.Entities.PatentLicenseActivity> Patents { get; set; }

		public IEnumerable<DAL.Entities.Membership> Memberships { get; set; }
	}
}
