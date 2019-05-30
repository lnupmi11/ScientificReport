using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintScientificInternship
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public ScientificInternship ScientificInternship { get; set; }
	}
}
