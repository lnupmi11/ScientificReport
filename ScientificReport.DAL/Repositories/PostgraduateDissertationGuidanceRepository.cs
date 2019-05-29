using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class PostgraduateDissertationGuidanceRepository: IRepository<PostgraduateDissertationGuidance>
	{
		private readonly ScientificReportDbContext _context;
		
		public PostgraduateDissertationGuidanceRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<PostgraduateDissertationGuidance> All()
		{
			return _context.PostgraduateDissertationGuidances.Include(pd => pd.Guide);
		}

		public virtual IEnumerable<PostgraduateDissertationGuidance> AllWhere(Func<PostgraduateDissertationGuidance, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual PostgraduateDissertationGuidance Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual PostgraduateDissertationGuidance Get(Func<PostgraduateDissertationGuidance, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(PostgraduateDissertationGuidance item)
		{
			_context.PostgraduateDissertationGuidances.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(PostgraduateDissertationGuidance item)
		{
			if (item != null)
			{
				_context.PostgraduateDissertationGuidances.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var user = _context.PostgraduateDissertationGuidances.Find(id);
			if (user != null)
			{
				_context.PostgraduateDissertationGuidances.Remove(user);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<PostgraduateDissertationGuidance> GetQuery()
		{
			return _context.PostgraduateDissertationGuidances;
		}
	}
}
