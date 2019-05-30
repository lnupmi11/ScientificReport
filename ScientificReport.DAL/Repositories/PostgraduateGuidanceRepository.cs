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
		
		public virtual IEnumerable<PostgraduateGuidance> All()
		{
			return _context.PostgraduateGuidances.Include(pg => pg.Guide);
		}

		public virtual IEnumerable<PostgraduateGuidance> AllWhere(Func<PostgraduateGuidance, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual PostgraduateGuidance Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual PostgraduateGuidance Get(Func<PostgraduateGuidance, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(PostgraduateGuidance item)
		{
			_context.PostgraduateGuidances.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(PostgraduateGuidance item)
		{
			if (item != null)
			{
				_context.PostgraduateGuidances.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.PostgraduateGuidances.Find(id);
			if (item != null)
			{
				_context.PostgraduateGuidances.Remove(item);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<PostgraduateGuidance> GetQuery()
		{
			return _context.PostgraduateGuidances;
		}
	}
}
