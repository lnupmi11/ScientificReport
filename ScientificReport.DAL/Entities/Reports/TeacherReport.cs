using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using ScientificReport.DAL.Entities.Publications;

namespace ScientificReport.DAL.Entities.Reports
{
	public class TeacherReport : Report
	{
		[Key]
		public override Guid Id { get; set; }

		public override DateTime Created { get; set; }
		public override DateTime Edited { get; set; }
		

		public UserProfile.UserProfile Teacher { get; set; }


		public ICollection<TeacherReportsPostgraduateDissertationGuidances> TeacherReportsPostgraduateDissertationGuidances { get; set; }
		public ICollection<TeacherReportsPostgraduateGuidances> TeacherReportsPostgraduateGuidances { get; set; }
		public ICollection<TeacherReportsPatents> TeacherReportsPatents { get; set; }
		public ICollection<TeacherReportsReviews> TeacherReportsReviews { get; set; }
		public ICollection<TeacherReportsOppositions> TeacherReportsOppositions { get; set; }
		public ICollection<TeacherReportsScientificConsultations> TeacherReportsScientificConsultations { get; set; }
		public ICollection<TeacherReportsArticles> TeacherReportsArticles { get; set; }
		public ICollection<TeacherReportsScientificWorks> TeacherReportsScientificWorks { get; set; }
		public ICollection<TeacherReportsPublications> TeacherReportsPublications { get; set; }
		public ICollection<TeacherReportsReportThesis> TeacherReportsReportThesis { get; set; }
		public ICollection<TeacherReportsGrants> TeacherReportsGrants { get; set; }
		public ICollection<TeacherReportsScientificInternships> TeacherReportsScientificInternships { get; set; }
		public ICollection<TeacherReportsMemberships> TeacherReportsMemberships { get; set; }

		public IEnumerable<ScientificWork> GetScientificWorks()
		{
			return TeacherReportsScientificWorks.Select(tr => tr.ScientificWork);
		}
		public IEnumerable<Publication> GetPublications()
		{
			return TeacherReportsPublications.Select(tr => tr.Publication);
		}
		public IEnumerable<Article> GetArticles()
		{

			return	TeacherReportsArticles.Select(tr => tr.Article);
		}
		public IEnumerable<ReportThesis> GetReportTheses()
		{
			return	TeacherReportsReportThesis.Select(tr => tr.ReportThesis);
		}
		
		public IEnumerable<Grant> GetGrants()
		{
			return  TeacherReportsGrants.Select(tr => tr.Grant);
		}
		public IEnumerable<ScientificInternship> GetScientificInternships()
		{
			return  TeacherReportsScientificInternships.Select(tr => tr.ScientificInternship);
		}
		public IEnumerable<PostgraduateGuidance> GetPostgraduateGuidances()
		{
			return  TeacherReportsPostgraduateGuidances.Select(tr => tr.PostgraduateGuidance);
		}
		public IEnumerable<ScientificConsultation> GetScientificConsultations()
		{
			return  TeacherReportsScientificConsultations.Select(tr => tr.ScientificConsultation);
		}
		public IEnumerable<PostgraduateDissertationGuidance> GetPostgraduateDissertationGuidances()
		{
			return  TeacherReportsPostgraduateDissertationGuidances.Select(tr => tr.PostgraduateDissertationGuidance);
		}
		public IEnumerable<Review> GetReviews()
		{
			return  TeacherReportsReviews.Select(tr => tr.Review);
		}
		public IEnumerable<Opposition> GetOppositions()
		{
			return  TeacherReportsOppositions.Select(tr => tr.Opposition);
		}
		
		public IEnumerable<Membership> GetMemberships()
		{
			return  TeacherReportsMemberships.Select(tr => tr.Membership);
		}
		public IEnumerable<Membership> GetMembershipsWithFilter(Func<Membership, int, bool> predicate)
		{
			return GetMemberships().Where(predicate);
		}
		public IEnumerable<Membership> GetMembershipsByType(Membership.Types type)
		{
			return GetMembershipsWithFilter((p, i) => p.Type == type);
		}

		public IEnumerable<PatentLicenseActivity> GetPatents()
		{
			return  TeacherReportsPatents.Select(tr => tr.Patent);
		}
		public IEnumerable<PatentLicenseActivity> GetPatentsWithFilter(Func<PatentLicenseActivity, int, bool> predicate)
		{
			return GetPatents().Where(predicate);
		}
		public IEnumerable<PatentLicenseActivity> GetPatentsByType(PatentLicenseActivity.Types type)
		{
			return GetPatentsWithFilter((p, i) => p.Type == type);
		}

		
		public IEnumerable<Conference> GetConferences()
		{
			return  TeacherReportsReportThesis.Select(tr => tr.ReportThesis.Conference);
		}
		public IEnumerable<Conference> GetConferencesWithFilter(Func<Conference, int, bool> predicate)
		{
			return GetConferences().Where(predicate);
		}
		public IEnumerable<Conference> GetConferencesByType(Conference.Types type)
		{
			return GetConferencesWithFilter((p, i) => p.Type == type);
		}

		public IEnumerable<Publication> GetPublicationsWithFilter(Func<Publication, int, bool> predicate)
		{
			return GetPublications().Where(predicate);
		}
		public IEnumerable<Publication> GetPublicationsByType(Publication.PublicationTypes type)
		{
			return GetPublicationsWithFilter((p, i) => p.PublicationType == type);
		}
		public IEnumerable<Publication> GetPublicationsOther()
		{
			return GetPublicationsWithFilter((p, i) => p.PublicationType == Publication.PublicationTypes.Other || p.PublicationType == Publication.PublicationTypes.Comment || p.PublicationType == Publication.PublicationTypes.BibliographicIndex );
		}
		
		public IEnumerable<Article> GetArticlesWithFilter(Func<Article, int, bool> predicate)
		{
			return GetArticles().Where(predicate);
		}
		public IEnumerable<Article> GetArticlesByType(Article.ArticleTypes type)
		{
			return GetArticlesWithFilter((p, i) => p.ArticleType == type);
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
