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

		public TeacherReportService(ScientificReportDbContext context)
		{
			_teacherReportRepository = new TeacherReportRepository(context);
			_articleRepository = new ArticleRepository(context);
			_scientificWorkRepository = new ScientificWorkRepository(context);
			_reportThesisRepository = new ReportThesisRepository(context);
			_publicationRepository = new PublicationRepository(context);
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
	}
}