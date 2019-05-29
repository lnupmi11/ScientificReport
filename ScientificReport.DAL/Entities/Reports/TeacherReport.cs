using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace ScientificReport.DAL.Entities.Reports
{
	public class TeacherReport : Report
	{
		[Key]
		public override Guid Id { get; set; }

		public override DateTime Created { get; set; }
		public override DateTime Edited { get; set; }

		public UserProfile.UserProfile Teacher { get; set; }


		public IEnumerable<Grant> Grants { get; set; }
		public IEnumerable<ScientificInternship> ScientificInternships { get; set; }
		public IEnumerable<PostgraduateDissertationGuidance> PostgraduateDissertationGuidances { get; set; }
		public IEnumerable<PostgraduateGuidance> PostgraduateGuidances { get; set; }
		public IEnumerable<PatentLicenseActivity> Patents { get; set; }
		public IEnumerable<Review> Reviews { get; set; }
		public IEnumerable<Opposition> Oppositions { get; set; }
		public IEnumerable<ScientificConsultation> ScientificConsultations { get; set; }
		
		public ICollection<TeacherReportsArticles> TeacherReportsArticles { get; set; }
		public ICollection<TeacherReportsScientificWorks> TeacherReportsScientificWorks { get; set; }
		public ICollection<TeacherReportsPublications> TeacherReportsPublications { get; set; }
		public ICollection<TeacherReportsReportThesis> TeacherReportsReportThesis { get; set; }
		
		public IEnumerable<ScientificWork> GetScientificWorks()
		{
			return TeacherReportsScientificWorks.Select(tr => tr.ScientificWork);
		}

		
		public IEnumerable<Publication> GetPublications()
		{
			return TeacherReportsPublications.Select(tr => tr.Publication);
		}

		public IEnumerable<Publication> GetPublicationsWithFilter(Func<Publication, int, bool> predicate)
		{
			return GetPublications().Where(predicate);
		}

		public IEnumerable<Publication> GetPublicationsByType(Publication.Types type)
		{
			return GetPublicationsWithFilter((p, i) => p.Type == type);
		}
		public IEnumerable<Publication> GetPublicationsOther()
		{
			return GetPublicationsWithFilter((p, i) => p.Type == Publication.Types.Other || p.Type == Publication.Types.Comment || p.Type == Publication.Types.BibliographicIndex );
		}

		public IEnumerable<Article> GetArticles()
		{

		return	TeacherReportsArticles.Select(tr => tr.Article);
		}

		public IEnumerable<Article> GetArticlesWithFilter(Func<Article, int, bool> predicate)
		{
			return GetArticles().Where(predicate);
		}

		public IEnumerable<Article> GetArticlesByType(Article.Types type)
		{
			return GetArticlesWithFilter((p, i) => p.Type == type);
		}
		
		public IEnumerable<ReportThesis> GetReportTheses()
		{
			return	TeacherReportsReportThesis.Select(tr => tr.ReportThesis);
		}

		public IEnumerable<ReportThesis> GetReportThesesWithFilter(Func<ReportThesis, int, bool> predicate)
		{
			return GetReportTheses().Where(predicate);
		}
		
		public IEnumerable<ReportThesis> GetReportThesesByConferenceType(Conference.Types type)
		{
			return GetReportThesesWithFilter((p, i) => p.Conference.Type == type);
		}

	}
}