using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintReportThesis
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public ReportThesis ReportThesis { get; set; }
	}
}