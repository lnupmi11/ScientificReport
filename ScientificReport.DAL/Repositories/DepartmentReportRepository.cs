using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
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
		
		public IEnumerable<DepartmentReport> All()
		{
			return _context.DepartmentReports
						.Include(b => b.TeacherReports)
						.Include(c => c.Conferences);
		}

		public IEnumerable<DepartmentReport> AllWhere(Func<DepartmentReport, bool> predicate)
		{
			return All().Where(predicate);
		}

		public DepartmentReport Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public DepartmentReport Get(Func<DepartmentReport, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(DepartmentReport item)
		{
			_context.DepartmentReports.Add(item);
			_context.SaveChanges();
		}

		public void Update(DepartmentReport item)
		{if (item != null)
			{
				_context.DepartmentReports.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.DepartmentReports.Find(id);
			if (user != null)
			{
				_context.DepartmentReports.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<DepartmentReport> GetQuery()
		{
			return _context.DepartmentReports;
		}
	}
}