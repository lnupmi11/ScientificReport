using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
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
		
		public IEnumerable<FacultyReport> All()
		{
			return _context.FacultyReports
						.Include(b => b.DepartmentReports);
		}

		public IEnumerable<FacultyReport> AllWhere(Func<FacultyReport, bool> predicate)
		{
			return All().Where(predicate);
		}

		public FacultyReport Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public FacultyReport Get(Func<FacultyReport, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(FacultyReport item)
		{
			_context.FacultyReports.Add(item);
			_context.SaveChanges();
		}

		public void Update(FacultyReport item)
		{if (item != null)
			{
				_context.FacultyReports.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.FacultyReports.Find(id);
			if (user != null)
			{
				_context.FacultyReports.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<FacultyReport> GetQuery()
		{
			return _context.FacultyReports;
		}
	}
}