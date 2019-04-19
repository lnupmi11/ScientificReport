using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ScientificInternshipRepository: IRepository<ScientificInternship>
	{
		private readonly ScientificReportDbContext _context;
		
		public ScientificInternshipRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<ScientificInternship> All()
		{
			return _context.ScientificInternships
						.Include(b => b.UserProfilesScientificInternships);
		}

		public IEnumerable<ScientificInternship> AllWhere(Func<ScientificInternship, bool> predicate)
		{
			return All().Where(predicate);
		}

		public ScientificInternship Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public ScientificInternship Get(Func<ScientificInternship, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(ScientificInternship item)
		{
			_context.ScientificInternships.Add(item);
			_context.SaveChanges();
		}

		public void Update(ScientificInternship item)
		{if (item != null)
			{
				_context.ScientificInternships.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.ScientificInternships.Find(id);
			if (user != null)
			{
				_context.ScientificInternships.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<ScientificInternship> GetQuery()
		{
			return _context.ScientificInternships;
		}
	}
}