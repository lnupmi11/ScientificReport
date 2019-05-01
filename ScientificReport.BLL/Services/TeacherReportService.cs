using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class TeacherReportService : ITeacherReportService
	{
		private readonly TeacherReportRepository _teacherReportRepository;

		public TeacherReportService(ScientificReportDbContext context)
		{
			_teacherReportRepository = new TeacherReportRepository(context);
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
	}
}
