using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class TeacherReport : Report
	{
		[Required]
		public virtual UserProfile Teacher { get; set; }
		
		public virtual ICollection<Publication> Publications { get; set; }
		
		public virtual ICollection<ScientificWork> ScientificWorks { get; set; }
		
		public virtual ICollection<Grant> Grants { get; set; }
		
		public virtual ICollection<ScientificInternship> ScientificInternships { get; set; }
		
		public virtual ICollection<ScientificGuidance> ScientificGuidances { get; set; }
		
		public virtual ICollection<StudentGuidance> StudentGuidances { get; set; }
		
		public virtual ICollection<ReportThesis> ReportTheses { get; set; }
		
		public virtual ICollection<Paper> Papers { get; set; }
		
		public virtual ICollection<Patent> Patents { get; set; }
		
		public virtual ICollection<Review> Reviews { get; set; }
		
		public virtual ICollection<ParticipationInScientificSitting> ParticipationInScientificSittings { get; set; }
		
		public ICollection<string> Other { get; set; }
	}
}
