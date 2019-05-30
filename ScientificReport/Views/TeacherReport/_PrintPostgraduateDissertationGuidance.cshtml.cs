using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintPostgraduateDissertationGuidance
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public PostgraduateDissertationGuidance PostgraduateDissertationGuidance { get; set; }
	}
}
