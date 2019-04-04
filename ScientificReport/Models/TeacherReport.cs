using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ScientificReport.Models
{
	public class TeacherReport : Report
	{
		[Required]
		public virtual UserProfile Teacher { get; set; }
		
		public virtual ICollection<ScientificWork> ScientificWorks { get; set; }
		
		public virtual ICollection<Grant> Grants { get; set; }
		
		public virtual ICollection<ScientificInternship> ScientificInternships { get; set; }
		
		// Scientific guidance field
		public virtual ICollection<PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }
		
		// Scientific guidance field
		public virtual ICollection<PostgraduateGuidance> PostgraduateGuidances { get; set; }
		
		// Scientific guidance field
		public virtual ICollection<ScientificConsultation> ScientificConsultations { get; set; }
		
		public virtual ICollection<Publication> Publications { get; set; }
		
		public virtual ICollection<ReportThesis> ReportTheses { get; set; }
		
		public virtual ICollection<PatentLicenseActivity> Patents { get; set; }
		
		public virtual ICollection<Review> Reviews { get; set; }
		
		public virtual ICollection<Membership> Memberships { get; set; }
		
		public virtual ICollection<Opposition> Oppositions { get; set; }
	}
}
