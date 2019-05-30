using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintConference
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public Conference Conference { get; set; }
	}
}
