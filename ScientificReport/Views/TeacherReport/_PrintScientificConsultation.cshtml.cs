using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintScientificConsultation
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public ScientificConsultation ScientificConsultation { get; set; }
	}
}
