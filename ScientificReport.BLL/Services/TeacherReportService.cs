using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using ScientificReport.BLL.Interfaces;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Repositories;

namespace ScientificReport.BLL.Services
{
	public class TeacherReportService : ITeacherReportService<Guid>
	{
		private readonly TeacherReportRepository _teacherReportRepository;

		public TeacherReportService(ScientificReportDbContext context)
		{
			_teacherReportRepository = new TeacherReportRepository(context);
		}

		public IEnumerable<TeacherReport> GetAll()
		{
			return _teacherReportRepository.All();
		}

		public IEnumerable<TeacherReport> GetAllWhere(Func<TeacherReport, bool> predicate)
		{
			return _teacherReportRepository.AllWhere(predicate);
		}

		public TeacherReport GetById(Guid id)
		{
			return _teacherReportRepository.Get(id);
		}

		public TeacherReport Get(Func<TeacherReport, bool> predicate)
		{
			return _teacherReportRepository.Get(predicate);
		}

		public void CreateItem(TeacherReport item)
		{
			_teacherReportRepository.Create(item);
		}

		public void UpdateItem(TeacherReport item)
		{
			_teacherReportRepository.Update(item);
		}

		public void DeleteById(Guid id)
		{
			_teacherReportRepository.Delete(id);
		}

		public bool Any(Func<TeacherReport, bool> predicate)
		{
			return _teacherReportRepository.AllWhere(predicate).Any();
		}

		public bool Exists(Guid id)
		{
			return Any(r => r.Id == id);
		}
	}
}