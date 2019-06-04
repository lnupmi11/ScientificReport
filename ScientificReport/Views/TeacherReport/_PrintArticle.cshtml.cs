using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Entities.UserProfile;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintArticle
	{
		public int Index { get; set; }
		public UserProfile User { get; set; }
		public Article Article { get; set; }
	}
}
