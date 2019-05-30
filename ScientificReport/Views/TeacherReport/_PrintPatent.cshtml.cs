using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintPatent
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public PatentLicenseActivity Patent { get; set; }
	}
}
