using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class PostgraduateGuidanceRepository: IRepository<PostgraduateGuidance>
	{
		private readonly ScientificReportDbContext _context;
		
		public PostgraduateGuidanceRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public IEnumerable<PostgraduateGuidance> All()
		{
			return _context.PostgraduateGuidances;
		}

		public IEnumerable<PostgraduateGuidance> AllWhere(Func<PostgraduateGuidance, bool> predicate)
		{
			return All().Where(predicate);
		}

		public PostgraduateGuidance Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public void Create(PostgraduateGuidance item)
		{
			_context.PostgraduateGuidances.Add(item);
			_context.SaveChanges();
		}

		public void Update(PostgraduateGuidance item)
		{if (item != null)
			{
				_context.PostgraduateGuidances.Update(item);
				_context.SaveChanges();
			}
		}

		public void Delete(Guid id)
		{
			var user = _context.PostgraduateGuidances.Find(id);
			if (user != null)
			{
				_context.PostgraduateGuidances.Remove(user);
				_context.SaveChanges();
			}
		}

		public IQueryable<PostgraduateGuidance> GetQuery()
		{
			return _context.PostgraduateGuidances;
		}
	}
}