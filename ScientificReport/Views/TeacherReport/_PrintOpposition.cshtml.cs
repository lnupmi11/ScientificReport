using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintOpposition
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public Opposition Opposition { get; set; }
	}
}
