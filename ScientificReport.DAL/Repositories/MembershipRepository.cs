using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class MembershipRepository: IRepository<Membership>
	{
		private readonly ScientificReportDbContext _context;
		
		public MembershipRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<Membership> All()
		{
			return _context.Memberships.Include(u => u.User);
		}

		public virtual IEnumerable<Membership> AllWhere(Func<Membership, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual Membership Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual Membership Get(Func<Membership, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(Membership item)
		{
			_context.Memberships.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(Membership item)
		{
			if (item != null)
			{
				_context.Memberships.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.Memberships.Find(id);
			if (item != null)
			{
				_context.Memberships.Remove(item);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<Membership> GetQuery()
		{
			return _context.Memberships;
		}
	}
}
