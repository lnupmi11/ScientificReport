using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ScientificReport.DAL.DbContext;
using ScientificReport.DAL.Entities;
using ScientificReport.DAL.Interfaces;

namespace ScientificReport.DAL.Repositories
{
	public class ReviewRepository: IRepository<Review>
	{
		private readonly ScientificReportDbContext _context;
		
		public ReviewRepository(ScientificReportDbContext context)
		{
			_context = context;
		}
		
		public virtual IEnumerable<Review> All()
		{
			return _context.Reviews
				.Include(r => r.Work)
				.Include(r => r.Reviewer);
		}

		public virtual IEnumerable<Review> AllWhere(Func<Review, bool> predicate)
		{
			return All().Where(predicate);
		}

		public virtual Review Get(Guid id)
		{
			return All().FirstOrDefault(u => u.Id == id);
		}

		public virtual Review Get(Func<Review, bool> predicate)
		{
			return All().Where(predicate).FirstOrDefault();
		}

		public virtual void Create(Review item)
		{
			_context.Reviews.Add(item);
			_context.SaveChanges();
		}

		public virtual void Update(Review item)
		{
			if (item != null)
			{
				_context.Reviews.Update(item);
				_context.SaveChanges();
			}
		}

		public virtual void Delete(Guid id)
		{
			var item = _context.Reviews.Find(id);
			if (item != null)
			{
				_context.Reviews.Remove(item);
				_context.SaveChanges();
			}
		}

		public virtual IQueryable<Review> GetQuery()
		{
			return _context.Reviews;
		}
	}
}
