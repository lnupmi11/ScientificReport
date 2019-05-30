using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintPostgraduateGuidance
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public PostgraduateGuidance PostgraduateGuidance { get; set; }
	}
}
