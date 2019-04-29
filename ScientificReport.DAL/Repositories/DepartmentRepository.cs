using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class DepartmentRepository: IRepository<Department>
	{
		private readonly ScientificReportDbContext _context;
		
		public DepartmentRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<Department> All()
		{
			return _context.Departments
				.Include(b => b.ScientificWorks)
				.Include(u => u.Staff)
				.Include(h => h.Head);
		}

		public IEnumerable<Department> AllWhere(Func<Department, bool> predicate)
		{
			return All().Where(predicate);
		}

		public Department Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public Department Get(Func<Department, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(Department item)
		{
			_context.Departments.Add(item);
			_context.SaveChanges();
		}

		public void Update(Department item)
		{
			if (item != null)
			{
				_context.Departments.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.Departments.Find(id);
			if (user != null)
			{
				_context.Departments.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<Department> GetQuery()
		{
			return _context.Departments;
		}
	}
}

