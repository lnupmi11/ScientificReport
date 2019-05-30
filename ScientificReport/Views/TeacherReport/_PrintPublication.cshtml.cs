using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintPublication
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public Publication Publication { get; set; }
	}
}