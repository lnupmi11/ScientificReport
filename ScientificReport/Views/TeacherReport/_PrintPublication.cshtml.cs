using ScientificReport.DAL.Entities;

namespace ScientificReport.Views.TeacherReport
{
	public class PrintPublication
	{
		public int Index { get; set; }
		public int User { get; set; }
		public Publication Publication { get; set; }
	}
}