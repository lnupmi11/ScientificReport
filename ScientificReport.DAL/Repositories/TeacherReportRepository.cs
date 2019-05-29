using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class TeacherReportRepository: IRepository<TeacherReport>
	{
		private readonly ScientificReportDbContext _context;

		public TeacherReportRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<TeacherReport> All()
		{
			return _context.TeacherReports
						.Include(r => r.Teacher)
						.Include(g => g.Grants)
						.Include(b=> b.Reviews)
						.Include(o=>o.Oppositions)
						.Include(p=>p.TeacherReportsPublications).ThenInclude(p => p.Publication)
						.Include(r=>r.ReportTheses)
						.Include(s=>s.ScientificWorks)
						.Include(p=>p.PostgraduateGuidances)
						.Include(sci=>sci.ScientificInternships)
						.Include(scc=>scc.ScientificConsultations)
						.Include(pdg=>pdg.PostgraduateDissertationGuidances)
						.Include(p=>p.Patents);
		}

		public virtual IEnumerable<TeacherReport> AllWhere(Func<TeacherReport, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual TeacherReport Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual TeacherReport Get(Func<TeacherReport, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(TeacherReport item)
		{
			_context.TeacherReports.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(TeacherReport item)
		{
			if (item == null) return;
			_context.TeacherReports.Update(item);
			_context.SaveChanges();
		}

		public virtual void Delete(Guid id)
		{
			var report = _context.TeacherReports.Find(id);
			if (report == null) return;
			_context.TeacherReports.Remove(report);
			_context.SaveChanges();
		}

		public virtual IQueryable<TeacherReport> GetQuery()
		{
			return _context.TeacherReports;
		}
	}
}
