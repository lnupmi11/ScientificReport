using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class GrantRepository: IRepository<Grant>
	{
		private readonly ScientificReportDbContext _context;
		
		public GrantRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<Grant> All()
		{
			return _context.Grants
						.Include(b => b.UserProfilesGrants)
						.ThenInclude(b => b.UserProfile);
		}

		public virtual IEnumerable<Grant> AllWhere(Func<Grant, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual Grant Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual Grant Get(Func<Grant, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(Grant item)
		{
			_context.Grants.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(Grant item)
		{if (item != null)
			{
				_context.Grants.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var user = _context.Grants.Find(id);
			if (user != null)
			{
				_context.Grants.Remove(user);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<Grant> GetQuery()
		{
			return _context.Grants;
		}
	}
}
