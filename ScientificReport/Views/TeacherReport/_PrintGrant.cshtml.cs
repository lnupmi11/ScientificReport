using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintGrant
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public Grant Grant { get; set; }
	}
}
