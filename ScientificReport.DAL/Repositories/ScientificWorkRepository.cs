using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities.Publications;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ScientificWorkRepository: IRepository<ScientificWork>
	{
		private readonly ScientificReportDbContext _context;
		
		public ScientificWorkRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<ScientificWork> All()
		{
			return _context.ScientificWorks
						.Include(b => b.UserProfilesScientificWorks)
						.ThenInclude(b => b.UserProfile);
		}

		public virtual IEnumerable<ScientificWork> AllWhere(Func<ScientificWork, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual ScientificWork Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual ScientificWork Get(Func<ScientificWork, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(ScientificWork item)
		{
			_context.ScientificWorks.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(ScientificWork item)
		{
			if (item == null) return;
			_context.ScientificWorks.Update(item);
			_context.SaveChanges();
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.ScientificWorks.Find(id);
			if (item == null)
			{
				return;
			}
			
			_context.ScientificWorks.Remove(item);
			_context.SaveChanges();
		}

		public virtual IQueryable<ScientificWork> GetQuery()
		{
			return _context.ScientificWorks;
		}
	}
}
