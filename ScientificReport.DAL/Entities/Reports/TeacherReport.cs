using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ScientificReport.DAL.Entities.Reports
{
	public class TeacherReport : Report
	{
		[Key]
		public Guid Id { get; set; }

		public DateTime Created { get; set; }
		public DateTime Edited { get; set; }

		public UserProfile.UserProfile Teacher { get; set; }

		public IEnumerable<ScientificWork> ScientificWorks { get; set; }

		public IEnumerable<Grant> Grants { get; set; }

		public IEnumerable<ScientificInternship> ScientificInternships { get; set; }

		// Scientific guidance field
		public IEnumerable<PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }

		// Scientific guidance field
		public IEnumerable<PostgraduateGuidance> PostgraduateGuidances { get; set; }

		// Scientific guidance field
		public IEnumerable<ScientificConsultation> ScientificConsultations { get; set; }

		public ICollection<TeacherReportsPublications> TeacherReportsPublications { get; set; }

		public IEnumerable<ReportThesis> ReportTheses { get; set; }

		public IEnumerable<PatentLicenseActivity> Patents { get; set; }

		public IEnumerable<Review> Reviews { get; set; }

		public IEnumerable<Membership> Memberships { get; set; }

		public IEnumerable<Opposition> Oppositions { get; set; }

		public IEnumerable<Publication> GetPublicationsWithFilter(Func<TeacherReportsPublications, int, bool> predicate)
		{
			return TeacherReportsPublications.Where(predicate).Select(tr => tr.Publication);
		}

		public IEnumerable<Publication> GetPublicationsByType(Publication.Types type)
		{
			return GetPublicationsWithFilter((tr, i) => tr.Publication.Type == type);
		}

	}
}
