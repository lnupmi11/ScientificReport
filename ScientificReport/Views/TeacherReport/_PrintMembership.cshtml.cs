using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintMembership
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public Membership Membership { get; set; }
	}
}
