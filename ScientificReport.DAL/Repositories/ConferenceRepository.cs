using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ConferenceRepository: IRepository<Conference>
	{
		private readonly ScientificReportDbContext _context;
		
		public ConferenceRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<Conference> All()
		{
			return _context.Conferences;
		}

		public IEnumerable<Conference> AllWhere(Func<Conference, bool> predicate)
		{
			return All().Where(predicate);
		}

		public Conference Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public Conference Get(Func<Conference, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(Conference item)
		{
			_context.Conferences.Add(item);
			_context.SaveChanges();
		}

		public void Update(Conference item)
		{if (item != null)
			{
				_context.Conferences.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.Conferences.Find(id);
			if (user != null)
			{
				_context.Conferences.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<Conference> GetQuery()
		{
			return _context.Conferences;
		}
	}
}