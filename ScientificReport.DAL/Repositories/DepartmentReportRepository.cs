using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class DepartmentReportRepository: IRepository<DepartmentReport>
	{
		private readonly ScientificReportDbContext _context;
		
		public DepartmentReportRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<DepartmentReport> All()
		{
			return _context.DepartmentReports
						.Include(b => b.TeacherReports)
						.Include(c => c.Conferences);
		}

		public virtual IEnumerable<DepartmentReport> AllWhere(Func<DepartmentReport, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual DepartmentReport Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual DepartmentReport Get(Func<DepartmentReport, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(DepartmentReport item)
		{
			_context.DepartmentReports.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(DepartmentReport item)
		{
			if (item != null)
			{
				_context.DepartmentReports.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.DepartmentReports.Find(id);
			if (item != null)
			{
				_context.DepartmentReports.Remove(item);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<DepartmentReport> GetQuery()
		{
			return _context.DepartmentReports;
		}
	}
}
