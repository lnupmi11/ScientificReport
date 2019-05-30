using System;
using System.Collections.Generic;
using System.Linq;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Entities.UserProfile;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class TeacherReportService : ITeacherReportService
	{
		private readonly TeacherReportRepository _teacherReportRepository;
		private readonly ArticleRepository _articleRepository;
		private readonly ScientificWorkRepository _scientificWorkRepository;
		private readonly ReportThesisRepository _reportThesisRepository;
		private readonly PublicationRepository _publicationRepository;

		private readonly GrantRepository _grantRepository;
		private readonly ScientificInternshipRepository _scientificInternshipRepository;
		private readonly PostgraduateGuidanceRepository _postgraduateGuidanceRepository;
		private readonly ScientificConsultationRepository _scientificConsultationRepository;
		private readonly PostgraduateDissertationGuidanceRepository _postgraduateDissertationGuidanceRepository;
		private readonly ReviewRepository _reviewRepository;
		private readonly OppositionRepository _oppositionRepository;
		private readonly PatentLicenseActivityRepository _patentRepository;
		private readonly MembershipRepository _membershipRepository;

		public TeacherReportService(ScientificReportDbContext context)
		{
			_teacherReportRepository = new TeacherReportRepository(context);
			_articleRepository = new ArticleRepository(context);
			_scientificWorkRepository = new ScientificWorkRepository(context);
			_reportThesisRepository = new ReportThesisRepository(context);
			_publicationRepository = new PublicationRepository(context);
			_grantRepository = new GrantRepository(context);
			_scientificInternshipRepository = new ScientificInternshipRepository(context);
			_postgraduateGuidanceRepository = new PostgraduateGuidanceRepository(context);
			_scientificConsultationRepository = new ScientificConsultationRepository(context);
			_postgraduateDissertationGuidanceRepository = new PostgraduateDissertationGuidanceRepository(context);
			_reviewRepository = new ReviewRepository(context);
			_oppositionRepository = new OppositionRepository(context);
			_patentRepository = new PatentLicenseActivityRepository(context);
			_membershipRepository = new MembershipRepository(context);
		}

		public virtual IEnumerable<TeacherReport> GetAll()
		{
			return _teacherReportRepository.All();
		}

		public virtual IEnumerable<TeacherReport> GetAllWhere(Func<TeacherReport, bool> predicate)
		{
			return _teacherReportRepository.AllWhere(predicate);
		}

		public virtual TeacherReport GetById(Guid id)
		{
			return _teacherReportRepository.Get(id);
		}

		public virtual TeacherReport Get(Func<TeacherReport, bool> predicate)
		{
			return _teacherReportRepository.Get(predicate);
		}

		public virtual void CreateItem(TeacherReport item)
		{
			_teacherReportRepository.Create(item);
		}

		public virtual void UpdateItem(TeacherReport item)
		{
			_teacherReportRepository.Update(item);
		}

		public virtual void DeleteById(Guid id)
		{
			_teacherReportRepository.Delete(id);
		}

		public virtual bool Any(Func<TeacherReport, bool> predicate)
		{
			return _teacherReportRepository.AllWhere(predicate).Any();
		}

		public virtual bool Exists(Guid id)
		{
			return Any(r => r.Id == id);
		}

		public void AddPublication(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _publicationRepository.Get(entityId);
			if (report.TeacherReportsPublications.Any(u => u.Publication.Id == entityId))
				return;

			report.TeacherReportsPublications.Add(new TeacherReportsPublications
			{
				TeacherReport = report,
				Publication = entity
			});
			UpdateItem(report);
		}

		public void RemovePublication(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsPublications.All(u => u.Publication.Id != entityId))
				return;


			report.TeacherReportsPublications = report.TeacherReportsPublications
				.Where(u => u.Publication.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddArticle(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _articleRepository.Get(entityId);
			if (report.TeacherReportsArticles.Any(u => u.Article.Id == entityId))
				return;

			report.TeacherReportsArticles.Add(new TeacherReportsArticles
			{
				TeacherReport = report,
				Article = entity
			});
			UpdateItem(report);
		}

		public void RemoveArticle(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsArticles.All(u => u.Article.Id != entityId))
				return;


			report.TeacherReportsArticles = report.TeacherReportsArticles
				.Where(u => u.Article.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddScientificWork(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _scientificWorkRepository.Get(entityId);
			if (report.TeacherReportsScientificWorks.Any(u => u.ScientificWork.Id == entityId))
				return;

			report.TeacherReportsScientificWorks.Add(new TeacherReportsScientificWorks
			{
				TeacherReport = report,
				ScientificWork = entity
			});
			UpdateItem(report);
		}

		public void RemoveScientificWork(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsScientificWorks.All(u => u.ScientificWork.Id != entityId))
				return;


			report.TeacherReportsScientificWorks = report.TeacherReportsScientificWorks
				.Where(u => u.ScientificWork.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddReportThesis(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _reportThesisRepository.Get(entityId);
			if (report.TeacherReportsReportThesis.Any(u => u.ReportThesis.Id == entityId))
				return;

			report.TeacherReportsReportThesis.Add(new TeacherReportsReportThesis
			{
				TeacherReport = report,
				ReportThesis = entity
			});
			UpdateItem(report);
		}

		public void RemoveReportThesis(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsReportThesis.All(u => u.ReportThesis.Id != entityId))
				return;


			report.TeacherReportsReportThesis = report.TeacherReportsReportThesis
				.Where(u => u.ReportThesis.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddGrant(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _grantRepository.Get(entityId);
			if (report.TeacherReportsGrants.Any(u => u.Grant.Id == entityId))
				return;

			report.TeacherReportsGrants.Add(new TeacherReportsGrants
			{
				TeacherReport = report,
				Grant = entity
			});
			UpdateItem(report);
		}

		public void RemoveGrant(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsGrants.All(u => u.Grant.Id != entityId))
				return;


			report.TeacherReportsGrants = report.TeacherReportsGrants
				.Where(u => u.Grant.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddScientificInternship(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _scientificInternshipRepository.Get(entityId);
			if (report.TeacherReportsScientificInternships.Any(u => u.ScientificInternship.Id == entityId))
				return;

			report.TeacherReportsScientificInternships.Add(new TeacherReportsScientificInternships
			{
				TeacherReport = report,
				ScientificInternship = entity
			});
			UpdateItem(report);
		}

		public void RemoveScientificInternship(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsScientificInternships.All(u => u.ScientificInternship.Id != entityId))
				return;


			report.TeacherReportsScientificInternships = report.TeacherReportsScientificInternships
				.Where(u => u.ScientificInternship.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddPostgraduateGuidance(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _postgraduateGuidanceRepository.Get(entityId);
			if (report.TeacherReportsPostgraduateGuidances.Any(u => u.PostgraduateGuidance.Id == entityId))
				return;

			report.TeacherReportsPostgraduateGuidances.Add(new TeacherReportsPostgraduateGuidances
			{
				TeacherReport = report,
				PostgraduateGuidance = entity
			});
			UpdateItem(report);
		}

		public void RemovePostgraduateGuidance(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsPostgraduateGuidances.All(u => u.PostgraduateGuidance.Id != entityId))
				return;


			report.TeacherReportsPostgraduateGuidances = report.TeacherReportsPostgraduateGuidances
				.Where(u => u.PostgraduateGuidance.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddScientificConsultation(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _scientificConsultationRepository.Get(entityId);
			if (report.TeacherReportsScientificConsultations.Any(u => u.ScientificConsultation.Id == entityId))
				return;

			report.TeacherReportsScientificConsultations.Add(new TeacherReportsScientificConsultations
			{
				TeacherReport = report,
				ScientificConsultation = entity
			});
			UpdateItem(report);
		}

		public void RemoveScientificConsultation(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsScientificConsultations.All(u => u.ScientificConsultation.Id != entityId))
				return;


			report.TeacherReportsScientificConsultations = report.TeacherReportsScientificConsultations
				.Where(u => u.ScientificConsultation.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddPostgraduateDissertationGuidance(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _postgraduateDissertationGuidanceRepository.Get(entityId);
			if (report.TeacherReportsPostgraduateDissertationGuidances.Any(u =>
				u.PostgraduateDissertationGuidance.Id == entityId))
				return;

			report.TeacherReportsPostgraduateDissertationGuidances.Add(
				new TeacherReportsPostgraduateDissertationGuidances
				{
					TeacherReport = report,
					PostgraduateDissertationGuidance = entity
				});
			UpdateItem(report);
		}

		public void RemovePostgraduateDissertationGuidance(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsPostgraduateDissertationGuidances.All(u =>
				u.PostgraduateDissertationGuidance.Id != entityId))
				return;


			report.TeacherReportsPostgraduateDissertationGuidances = report
				.TeacherReportsPostgraduateDissertationGuidances
				.Where(u => u.PostgraduateDissertationGuidance.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddReview(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _reviewRepository.Get(entityId);
			if (report.TeacherReportsReviews.Any(u => u.Review.Id == entityId))
				return;

			report.TeacherReportsReviews.Add(new TeacherReportsReviews
			{
				TeacherReport = report,
				Review = entity
			});
			UpdateItem(report);
		}

		public void RemoveReview(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsReviews.All(u => u.Review.Id != entityId))
				return;


			report.TeacherReportsReviews = report.TeacherReportsReviews
				.Where(u => u.Review.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddOpposition(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _oppositionRepository.Get(entityId);
			if (report.TeacherReportsOppositions.Any(u => u.Opposition.Id == entityId))
				return;

			report.TeacherReportsOppositions.Add(new TeacherReportsOppositions
			{
				TeacherReport = report,
				Opposition = entity
			});
			UpdateItem(report);
		}

		public void RemoveOpposition(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsOppositions.All(u => u.Opposition.Id != entityId))
				return;


			report.TeacherReportsOppositions = report.TeacherReportsOppositions
				.Where(u => u.Opposition.Id != entityId).ToList();
			UpdateItem(report);
		}

		public void AddPatent(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _patentRepository.Get(entityId);
			if (report.TeacherReportsPatents.Any(u => u.Patent.Id == entityId))
				return;

			report.TeacherReportsPatents.Add(new TeacherReportsPatents
			{
				TeacherReport = report,
				Patent = entity
			});
			UpdateItem(report);
		}

		public void RemovePatent(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsPatents.All(u => u.Patent.Id != entityId))
				return;


			report.TeacherReportsPatents = report.TeacherReportsPatents
				.Where(u => u.Patent.Id != entityId).ToList();
			UpdateItem(report);
		}
		
		public void AddMembership(Guid id, Guid entityId)
		{
			var report = GetById(id);
			var entity = _membershipRepository.Get(entityId);
			if (report.TeacherReportsMemberships.Any(u => u.Membership.Id == entityId))
				return;

			report.TeacherReportsMemberships.Add(new TeacherReportsMemberships
			{
				TeacherReport = report,
				Membership = entity
			});
			UpdateItem(report);
		}

		public void RemoveMembership(Guid id, Guid entityId)
		{
			var report = _teacherReportRepository.Get(id);
			if (report.TeacherReportsMemberships.All(u => u.Membership.Id != entityId))
				return;


			report.TeacherReportsMemberships = report.TeacherReportsMemberships
				.Where(u => u.Membership.Id != entityId).ToList();
			UpdateItem(report);
		}

	}
}