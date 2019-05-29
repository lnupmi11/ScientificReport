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
		private readonly ScientificWorkRepository _scientificWorkRepository;
		private readonly PublicationRepository _publicationRepository;

		public TeacherReportService(ScientificReportDbContext context)
		{
			_teacherReportRepository = new TeacherReportRepository(context);
			_scientificWorkRepository = new ScientificWorkRepository(context);
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
	}
}