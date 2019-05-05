using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Reports;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class FacultyReportRepository: IRepository<FacultyReport>
	{
		private readonly ScientificReportDbContext _context;
		
		public FacultyReportRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<FacultyReport> All()
		{
			return _context.FacultyReports
						.Include(b => b.DepartmentReports);
		}

		public virtual IEnumerable<FacultyReport> AllWhere(Func<FacultyReport, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual FacultyReport Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual FacultyReport Get(Func<FacultyReport, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(FacultyReport item)
		{
			_context.FacultyReports.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(FacultyReport item)
		{
			if (item != null)
			{
				_context.FacultyReports.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var user = _context.FacultyReports.Find(id);
			if (user != null)
			{
				_context.FacultyReports.Remove(user);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<FacultyReport> GetQuery()
		{
			return _context.FacultyReports;
		}
	}
}
