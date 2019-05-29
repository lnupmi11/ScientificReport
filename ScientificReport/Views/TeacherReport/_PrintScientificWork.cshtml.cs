using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintScientificWork
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public ScientificWork ScientificWork { get; set; }
	}
}