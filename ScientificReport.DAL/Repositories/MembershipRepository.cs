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
		
		public IEnumerable<Membership> All()
		{
			return _context.Membership;
		}

		public IEnumerable<Membership> AllWhere(Func<Membership, bool> predicate)
		{
			return All().Where(predicate);
		}

		public Membership Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public Membership Get(Func<Membership, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(Membership item)
		{
			_context.Membership.Add(item);
			_context.SaveChanges();
		}

		public void Update(Membership item)
		{if (item != null)
			{
				_context.Membership.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.Membership.Find(id);
			if (user != null)
			{
				_context.Membership.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<Membership> GetQuery()
		{
			return _context.Membership;
		}
	}
}